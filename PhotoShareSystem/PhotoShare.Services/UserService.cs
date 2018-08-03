using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper.QueryableExtensions;
using PhotoShare.Data;
using PhotoShare.Models;
using PhotoShare.Services.Contracts;

namespace PhotoShare.Services
{
    public class UserService : IUserService
    {
        private readonly PhotoShareContext context;
        //private readonly ITownService townService;

        private const string TownNotFound = "Town not found!";
        private const string AlreadyFriends = "{0} is already a friend to {1}";
        private const string NoSuchFriendRequest = "{0} has not added {1} as a friend";
        private const string NoFriends = "No friends for this user. :(";

        public UserService(PhotoShareContext context)
        {
            this.context = context;
            //this.townService = townService;
        }

        public TModel ById<TModel>(int id)
            => By<TModel>(p => p.Id == id).SingleOrDefault();

        TModel IUserService.ById<TModel>(int id)
            => By<TModel>(p => p.Id == id).SingleOrDefault();

        public TModel ByUsername<TModel>(string username)
            => By<TModel>(p => p.Username == username).SingleOrDefault();

        public TModel ByUsernameAndPassword<TModel>(string username, string password)
            => By<TModel>(p => p.Username == username && p.Password == password).SingleOrDefault();

        public bool Exists(int id)
         => ById<User>(id) != null;

        public bool Exists(string name)
            => ByUsername<User>(name) != null;

        public User Register(string username, string password, string email)
        {
            var user = new User()
            {
                Username = username,
                Email = email,
                Password = password
            };

            context.Add(user);
            context.SaveChanges();
            return user;
        }

        public void Delete(string username)
        {

            if (!Exists(username))
            {
                throw new ArgumentException("Invalid username!");
            }

            var user = ByUsername<User>(username);

            user.IsDeleted = true;
            context.SaveChanges();
        }

        public Friendship AddFriend(int userId, int friendId)
        {
            var user = ById<User>(userId);
            var friend = ById<User>(friendId);

            if (context.Friendships.Any(p => p.UserId == userId && p.FriendId == friendId))
            {
                throw new ArgumentException(String.Format(AlreadyFriends, user.Username, friend.Username));
            }

            var friendship = new Friendship()
            {
                User = user,
                UserId = userId,
                Friend = friend,
                FriendId = friendId

            };

           // friend.FriendsAdded.Add(friendship);

            context.Add(friendship);
            context.SaveChanges();

            return friendship;
        }

        public Friendship AcceptFriend(int invited, int invitor)
        {
            //if (!Exists(invited) || !Exists(invitor))
            //{
            //    throw new ArgumentException("User or friend does not exist.");
            //}

            var invitedUser = ById<User>(invited);
            var userWhoRequests = ById<User>(invitor);

            if (context.Friendships.Any(p => p.UserId == invited && p.FriendId == invitor))
            {
                throw new ArgumentException(String.Format(AlreadyFriends, invitedUser.Username, userWhoRequests.Username));
            }

            if (!context.Friendships.Any(p => p.UserId == invitor && p.FriendId == invited))
            {
                throw new ArgumentException(String.Format(NoSuchFriendRequest, userWhoRequests.Username, invitedUser.Username));
            }

            var friendship = new Friendship()
            {
                User = invitedUser,
                UserId = invited, 
                Friend = userWhoRequests, 
                FriendId = invitor
               
            };

            invitedUser.FriendsAdded.Add(friendship);

            context.Add(friendship);
            context.SaveChanges();

            return friendship;
        }

        public void ChangePassword(int userId, string password)
        {
            if (!Exists(userId))
            {
                throw new ArgumentException("Invalid user id!");
            }

            var user = ById<User>(userId);
            user.Password = password;
            context.SaveChanges();
        }

        public void SetBornTown(int userId, string townName)
        {
            if (!Exists(userId))
            {
                throw new ArgumentException("Invalid user id!");
            }

            var town = context.Towns.SingleOrDefault(p => p.Name == townName);

            if (town == null)
            {
                throw new ArgumentException(TownNotFound);
            }

            var user = ById<User>(userId);
            user.BornTown = town;
            context.SaveChanges();
        }

        public void SetCurrentTown(int userId, string townName)
        {
            if (!Exists(userId))
            {
                throw new ArgumentException("Invalid user id!");
            }

            var town = context.Towns.SingleOrDefault(p => p.Name == townName);

            if (town == null)
            {
                throw new ArgumentException(TownNotFound);
            }

            var user = ById<User>(userId);
            user.CurrentTown = town;
            context.SaveChanges();
        }

        public string ListFriends(int userId)
        {
            var sb = new StringBuilder();

            if (!Exists(userId))
            {
                throw new ArgumentException("Invalid user id!");
            }

            var friendships = context.Friendships
                .Where(p => p.UserId == userId)
                .Select(p =>p.Friend.Username);

            if (!friendships.Any())
            {
                return NoFriends;
            }

            foreach (var friend in friendships)
            {
                sb.AppendLine($"-{friend}");
            }

            return sb.ToString().Trim();
        }

        private IEnumerable<TModel> By<TModel>(Func<User, bool> predicate)
            => this.context
                .Users
                .Where(predicate)
                .AsQueryable()
                .ProjectTo<TModel>();
    }
}