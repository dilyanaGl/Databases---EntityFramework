using System;
using System.Collections.Generic;
using System.Text;
using Office.App.Contracts;

namespace Office.App.Commands
{
    public class LogoutCommand : ICommand
    {
        private const string SuccessMessage = "User {0} successfully logged out!";
        private const string NoUser = "You should login first!";
        private const string InvalidLength = "Invalid arguments count!";

        public string Execute(string[] data)
        {
            if (!Validation.CheckLength(0, data))
            {
                return InvalidLength;
            }

            if (Session.User == null)
            {

                return NoUser;
            }

            var username = Session.User.Username;

            Session.User = null;

            return String.Format(SuccessMessage, username);
        }

     
    }
}
