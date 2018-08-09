using System;
using System.Collections.Generic;
using System.Text;
using Office.App.Contracts;
using Office.Models;
using Office.Services.Contracts;

namespace Office.App.Commands
{
    public class LoginCommand : ICommand
    {
        private const string SuccessMessage = "User {0} successfully logged in!";
        private const string InvalidDetails = "Invalid username or password!";
        private const string UserLoggedIn = "You should logout first!";
        private const string InvalidLength = "Invalid arguments count!";

        private readonly IUserService userService;

        public LoginCommand(IUserService userService)
        {
            this.userService = userService;
        }


        public string Execute(string[] data)
        {
            if (!Validation.CheckLength(2, data))
            {
                return InvalidLength;
            }

            string username = data[0];
            string password = data[1];

            if (Session.User != null && Session.User.Username == username)
            {

                return UserLoggedIn;

            }

            if (!userService.Exist(username, password))
            {

                return InvalidDetails;
            }

            var user = userService.ByUsername(username);

            Session.User = user;

            return String.Format(SuccessMessage, username);

        }
    }
}
