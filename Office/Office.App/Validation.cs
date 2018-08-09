using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace Office.App
{
    public static class Validation
    {
        public static bool ValidateUserame(string username)
        {
            return !String.IsNullOrWhiteSpace(username) && username.Length >= 3 && username.Length <= 25;
        }

        public static bool ValidatePassword(string password)
        {
            return !String.IsNullOrWhiteSpace(password) && Regex.IsMatch(password, @"^(?=.*?[0-9])(?=.*[A-Z]).{6,12}$");
        }

        public static bool ValidatePasswordsMatch(string password, string repeatPassword)
        {
            return password.Equals(repeatPassword);
        }

        public static bool ValidateAge(int age)
        {
            return age >= 0;
        }

        public static bool ValidateGender(string gender)
        {
            return gender.Equals("Female", StringComparison.InvariantCultureIgnoreCase) ||
                   gender.Equals("Male", StringComparison.InvariantCultureIgnoreCase);
        }

        public static bool IsValid(object obj)
        {
            var validationContext = new ValidationContext(obj);
            var validationResult = new List<ValidationResult>();
            return Validator.TryValidateObject(obj, validationContext, validationResult, true);
        }

        public static bool ValidateDate(string dateString)
        {
             DateTime date;
           return DateTime.TryParseExact(dateString, "dd/MM/yyyyHH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None,
                out date);

        }

        public static bool ValidateAcronym(string acronym)
        {
            return acronym.Length == 3;
        }

        public static bool CheckLength(int length, string[] arr)
        {
            return arr.Length == length;
        }

    }
}
