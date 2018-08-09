using System;
using System.Collections.Generic;
using System.Text;
using Office.App.Contracts;
using Office.App.Dto;
using Office.Services.Contracts;

namespace Office.App.Commands
{
    public class RegisterUserCommand : ICommand
    {
        private const string InvalidGender = "Gender should be either \"Male\" or \"Female\"";
        private const string InvalidAge = "Age not valid!";
        private const string SuccessMessage = "Username {0} was registered successfully!";
        private const string InvalidUsername = "Username {0} not valid!";
        private const string InvalidPassword = "Password {0} not valid!";
        private const string UsernameTaken = "Username {0} is already taken!";
        private const string LogOutFirst = "You should logout first!";
        private const string PasswordsNotMatch = "Passwords do not match!";
        private const string InvalidDetails = "Invalid details!";
        private const string InvalidLength = "Invalid arguments count!";


        private readonly IUserService userService;

        public RegisterUserCommand(IUserService userService)
        {

            this.userService = userService;

        }


        public string Execute(string[] data)
        {
            if (!Validation.CheckLength(7, data))
            {
                return InvalidLength;
            }


            ; var registerDto = new RegisterUserDto
            {
                Username = data[0],
                Password = data[1],
                RepeatPassword = data[2],
                FirstName = data[3],
                LastName = data[4],
                Age = int.Parse(data[5]),
                Gender = data[6]
            };

            if (Session.User != null && Session.User.Username == registerDto.Username)
            {

                return LogOutFirst;
            }

            if (userService.Exist(registerDto.Username))
            {
                return String.Format(UsernameTaken, registerDto.Username);

            }

            if (!Validation.ValidateUserame(registerDto.Username))
            {
                return String.Format(InvalidUsername, registerDto.Username);
                ;
            }

            if (!Validation.ValidatePassword(registerDto.Password))
            {
                return String.Format(InvalidPassword, registerDto.Password);
            }

            if (!Validation.ValidatePasswordsMatch(registerDto.Password, registerDto.RepeatPassword))
            {
                return PasswordsNotMatch;

            }
            if (!Validation.ValidateAge(registerDto.Age))
            {
                return InvalidAge;
            }

            if (!Validation.ValidateGender(registerDto.Gender))
            {

                return InvalidGender;

            }

            if (!Validation.IsValid(registerDto))
            {
                return InvalidDetails;
            }

            userService.AddUser(registerDto.Username, registerDto.Password, registerDto.FirstName, registerDto.LastName, registerDto.Age, registerDto.Gender);

            return String.Format(SuccessMessage, registerDto.Username);

        }
    }
}
