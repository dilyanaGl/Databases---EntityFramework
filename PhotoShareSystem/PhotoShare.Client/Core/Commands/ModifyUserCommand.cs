using System.Linq;
using System.Reflection;
using PhotoShare.Models;
using PhotoShare.Services;
using PhotoShare.Services.Contracts;

namespace PhotoShare.Client.Core.Commands
{
    using System;
    using Contracts;

    public class ModifyUserCommand : ICommand
    {
        private const string SuccessMessage = "User {0} {1} is {2}.";
        private const string UserNotFound = "User {0} not found!";
        private const string PropertyNotSupported = "Property {0} not supported!";
        private const string ValueNotSupported = "Value {0} not valid.";
        private const string OperationNotAllowed = "Invalid Credentials!";


        private readonly IUserService userService;

        public ModifyUserCommand(IUserService userService)
        {
            this.userService = userService;
        }

        // ModifyUser <username> <property> <new value>
        // For example:
        // ModifyUser <username> Password <NewPassword>
        // ModifyUser <username> BornTown <newBornTownName>
        // ModifyUser <username> CurrentTown <newCurrentTownName>
        // !!! Cannot change username
        public string Execute(string[] data)
        {
            var username = data[0];
            var property = data[1];
            var value = data[2];

            if (username != Session.CurrentUser.Username)
            {
                return OperationNotAllowed;
            }

            if (!userService.Exists(username))
            {
                return UserNotFound;
            }

            var user = userService.ByUsername<User>(username);

            var method = typeof(IUserService)
                .GetMethods(BindingFlags.Instance | BindingFlags.Public)
                .FirstOrDefault(p => p.Name.Contains(property));

            if (method == null)
            {
                return String.Format(PropertyNotSupported, property);
            }

            try
            {
                method.Invoke(userService, new object[] { user.Id, value});
            }
            catch(Exception ex)
            {
                return String.Format(ValueNotSupported, value) + " " + ex.InnerException.Message;
            }

            return String.Format(SuccessMessage, username, property, value);
        }
    }
}
