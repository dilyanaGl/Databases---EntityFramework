using System;
using System.Collections.Generic;
using System.Text;
using PhotoShare.Client.Core.Contracts;
using PhotoShare.Models;
using PhotoShare.Services.Contracts;

namespace PhotoShare.Client.Core.Commands
{
    public class LoginCommand : ICommand
    {
        private const string SuccessMessage = "User {0} successfully logged in!";
        private const string LogoutFirst = "You should log out first!";
        private const string InvalidDetails = "Invalid username or password!";

        private readonly IUserService userService;

        public LoginCommand(IUserService userService)
        {
            this.userService = userService;
        }

        public string Execute(string[] args)
        {
            string username = args[0];
            string password = args[1];


            if (Session.CurrentUser != null && username == Session.CurrentUser.Username)
            {
                return LogoutFirst;
            }

            var user = userService.ByUsernameAndPassword<User>(username, password);

            if (user == null)
            {
                return InvalidDetails;
            }

            Session.CurrentUser = user;

            return String.Format(SuccessMessage, username);
        }
    }
}
