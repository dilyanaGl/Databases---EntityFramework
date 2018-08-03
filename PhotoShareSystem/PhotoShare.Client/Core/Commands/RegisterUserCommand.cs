using System.Linq;
using AutoMapper;
using PhotoShare.Client.Core.Dtos;
using PhotoShare.Data;
using PhotoShare.Models;
using PhotoShare.Services.Contracts;

namespace PhotoShare.Client.Core.Commands
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Contracts;

    public class RegisterUserCommand : ICommand
    {
        private const string SuccessMessge = "User {0} was registered successfully!";
        private const string UsernameTaken = "Username {0} is already taken!";
        private const string PasswordsNotMatch = "Passwords do not match!";
        private const string InvalidDetails = "Invalid details!";

        private readonly IUserService userService;

        public RegisterUserCommand(IUserService userService)
        {
            this.userService = userService;
        }

        // RegisterUser <username> <password> <repeat-password> <email>
        public string Execute(string[] data)
        {
            var userDto = new RegisterUserDto()
            {
                Username = data[0],
                Password = data[1],
                RepeatPassword = data[2],
                Email = data[3]

            };

            if (!IsValid(userDto))
            {
                return InvalidDetails;
            }

            if (userService.Exists(userDto.Username))
            {
                return String.Format(UsernameTaken, userDto.Username);
            }

            if (!userDto.Password.Equals(userDto.RepeatPassword))
            {
                return PasswordsNotMatch;
            }

            try
            {
                userService.Register(userDto.Username, userDto.Password, userDto.Email);
            }
            catch (Exception ex)
            {
                return ex.Message + ex.InnerException;
            }

            return String.Format(SuccessMessge, userDto.Username);
        }

        private bool IsValid(object obj)
        {
            var validationContext = new ValidationContext(obj);
            var validationResult = new List<ValidationResult>();
            return Validator.TryValidateObject(obj, validationContext, validationResult,  true);
        }

    
    }
}
