using PhotoShare.Models;
using PhotoShare.Services.Contracts;

namespace PhotoShare.Client.Core.Commands
{
    using System;

    using Contracts;

    public class AddFriendCommand : ICommand
    {
        private const string SuccessMessage = "Friend {0} added to {1}!";
        private const string UserNotFound = "{0} not found!";
        private const string CannotBefriendYoursef = "User {0} cannot befriend themselves!";
        private const string OperationNotAllowed = "Invalid Credentials!";


        private readonly IUserService userService;

        public AddFriendCommand(IUserService userService)
        {
            this.userService = userService;
        }

        // AddFriend <username1> <username2>
        public string Execute(string[] data)
        {
            var username = data[0];
            var friendName = data[1];

            if (username != Session.CurrentUser.Username)
            {
                return OperationNotAllowed;
            }

            if (username == friendName)
            {
                return String.Format(CannotBefriendYoursef, username);
            }

            var user = userService.ByUsername<User>(username);
            var friend = userService.ByUsername<User>(friendName);
            if (user == null)
            {
                return String.Format(UserNotFound, username);
            }

            if (friend == null)
            {
                return String.Format(UserNotFound, friendName);
            }

            try
            {
                userService.AddFriend(user.Id, friend.Id);
            }
            catch(Exception ex)
            {
                return ex.Message;
            }

            return String.Format(SuccessMessage, username, friendName);
        }
    }
}
