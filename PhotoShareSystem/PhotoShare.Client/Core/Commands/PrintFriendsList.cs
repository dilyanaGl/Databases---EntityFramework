using System;
using PhotoShare.Client.Core.Contracts;
using PhotoShare.Models;
using PhotoShare.Services.Contracts;

namespace PhotoShare.Client.Core.Commands
{
    public class PrintFriendsListCommand : ICommand
    {
        private const string UserNotFound = "{0} not found!";


        private readonly IUserService userService;

        public PrintFriendsListCommand(IUserService userService)
        {
            this.userService = userService;
        }

        public string Execute(string[] args)
        {
            var username = args[0];

            if (!userService.Exists(username))
            {
                return String.Format(UserNotFound, username);
            }

            var user = userService.ByUsername<User>(username); 

            return userService.ListFriends(user.Id);
        }
    }
}
