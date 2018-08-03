using System;
using System.Collections.Generic;
using System.Text;
using PhotoShare.Client.Core.Contracts;

namespace PhotoShare.Client.Core.Commands
{
    public class LogoutCommand : ICommand
    {
        private const string SuccessMessage = "User {0} successfully logged out!";
        private const string LogInFirst = "You should log in first in order to logout.";

        public string Execute(string[] args)
        {
            if (Session.CurrentUser == null)
            {
                return LogInFirst;
            }

            string username = Session.CurrentUser.Username;

            Session.CurrentUser = null;

            return String.Format(SuccessMessage, username);
        }
    }
}
