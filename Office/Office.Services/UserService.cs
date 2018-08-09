using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper.QueryableExtensions;
using Office.Data;
using Office.Models;
using Office.Models.Enums;
using Office.Services.Contracts;


namespace Office.Services
{
    public class UserService : IUserService
    {
        private readonly OfficeContext context;

        public UserService(OfficeContext context)
        {
            this.context = context;

        }

        public User GetsUserByUsernameAndPassword(string username, string password)
        {
            if (!Exist(username, password))
            {
                throw new ArgumentException("Invalid username or password!");
            }

            User user = context.Users.First(u => u.Username == username);

            return user;
        }


        public TModel ById<TModel>(int id)
            => By<TModel>(p => p.Id == id).SingleOrDefault();

        public User ByUsername(string username)
            => context.Users.SingleOrDefault(p => p.Username == username);


        public bool Exist(string username, string password)
        {
            return context.Users.Any(u => u.Username == username && u.Password == password);
        }

        public bool Exist(string username)
        {
            return context.Users.Any(u => u.Username == username);
        }

        public void AddUser(string username, string password, string firstName, string lastName, int age, string genderString)
        {
            var gender = Enum.Parse<Gender>(genderString);

            var user = new User
            {
                Username = username,
                Password = password,
                FirstName = firstName,
                LastName = lastName,
                Age = age,
                Gender = gender


            };

            context.Users.Add(user);
            context.SaveChanges();


        }

        public void Delete(string username)
        {

            if (!Exist(username))
            {
                throw new ArgumentException("Invalid username!");
            }

            var user = ByUsername(username);

            user.IsDeleted = true;
            context.SaveChanges();
        }


        private IEnumerable<TModel> By<TModel>(Func<User, bool> predicate)
            => this.context
                .Users
                .Where(predicate)
                .AsQueryable()
                .ProjectTo<TModel>();

    }


}

