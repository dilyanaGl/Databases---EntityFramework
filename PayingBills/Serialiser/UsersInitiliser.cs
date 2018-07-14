using System;
using System.Collections.Generic;
using System.Text;
using P01_BillsPaymentSystem.Data.Models;

namespace Serialiser
{
    public static class UsersInitiliser
    {
        private static string[] names = new string[]
        {
            "Atanas",
            "Loud",
            "Teresa",
            "Lola",
            "Ivan",
            "Pesho",
            "Elena",
            "Yordan",
            "Rust",
            "Marty",
            "Laura",
            "Audrey"
        };

        private static string[] lastNames = new string[]
        {
            "Ivanov",
            "Smith",
            "Jones",
            "Cooper",
            "Cole",
            "Witgenstein",
            "Lynch",
            "Cohle",
            "Horne",
            "Palmer"

        };

       private static string[] passwords = new string[]
        {
            "23ee",
            "ds4434",
            "dsd42424",
            "dsd4232", 
            "sds3434df",
            "sds3434dfsas"
        };

        private static string[] emailEndings = new string[]
        {
            "abv.bg",
            "gmail.com",
            "yahoo.com"
        };

        public static User[] GetUsers()
        {
            var users = new User[10];
            var rnd = new Random();

            for (int i = 0; i < users.Length; i++)
            {
                var firstName = names[rnd.Next(0, names.Length - 1)];
                var lastName = lastNames[rnd.Next(0, lastNames.Length - 1)];
                var emailEnding = emailEndings[rnd.Next(0, emailEndings.Length - 1)];
                var email = String.Format("{0}.{1}@{2}", firstName, lastName, emailEnding);
                var password = passwords[rnd.Next(0, passwords.Length - 1)];

                var user = new User()
                {
                    //UserId = i + 1, 
                    FirstName = firstName,
                    LastName = lastName, 
                    Email = email,
                    Password = password

                };
                users[i] = user;
            }


            return users;
        }
    }
}
