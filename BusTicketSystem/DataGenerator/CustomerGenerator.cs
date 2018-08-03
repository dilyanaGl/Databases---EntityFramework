using System;
using System.Collections.Generic;
using System.Text;
using BusTicketSystem.Data;
using BusTicketSystem.Models;
using BusTicketSystem.Models.Enums;

namespace DataGenerator
{
    public static class CustomerGenerator
    {
        public static void GenerateCustomers(BusTicketContext context)
        {
            var firstNames = new string[]{

                "Dale",
                "Laura",
                "Dominic",
                "Gordon",
                "Diane",
                "Donna",
                "Audrey",
                "Ben",
                "Marty"

            };

            var lastNames = new string[]
            {
                "Cole",
                "Cooper",
                "Hart",
                "West",
                "Bell",
                "McNulty",
                "Horne",
                "Palmer"
            };

            var validCustomers = new List<Customer>();
            var rnd = new Random();


            for (int i = 0; i < firstNames.Length; i++)
            {


                var lastNameIndex = rnd.Next(0, lastNames.Length - 1);

                var daysToSubtract = rnd.Next(1000, 10000);

                var customer = new Customer
                {
                    FirstName = firstNames[i],
                    LastName = lastNames[lastNameIndex],
                    BirthDate = DateTime.Now.AddDays(daysToSubtract * -1),
                    Gender = (Gender)(i % 3)
                };


                validCustomers.Add(customer);

            }

            context.AddRange(validCustomers);
            context.SaveChanges();
        }
    }
}
