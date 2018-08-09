using System;
using System.Collections.Generic;
using System.Text;
using Office.App.Contracts;
using Office.Services.Contracts;

namespace Office.App.Commands
{
    public class DeleteUserCommand : ICommand
    {
        private const string SuccessMessage = "User {0} was deleted successfully!";
        private const string NoUser = "You should login first!";
        private const string InvalidLength = "Invalid arguments count!";

        private readonly IUserService userService;

        public DeleteUserCommand(IUserService userService)
        {

            this.userService = userService;

        }

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

            userService.Delete(username);

            return String.Format(SuccessMessage, username);


        }
    }
}
