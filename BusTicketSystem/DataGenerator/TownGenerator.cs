using System;
using System.Collections.Generic;
using BusTicketSystem.Data;
using BusTicketSystem.Models;

namespace DataGenerator
{
    public static class TownGenerator
    {

        public static void GenerateTowns(BusTicketContext context)
        {
            string[] townNames = new string[]
            {

                "Longdale",
                "StringDale",
                "SpringField",
                "Lodingdon",
                "AppleTown",
                "GeorgeTown",
                "Misty Lake"
            };


            string[] countries = new string[]
            {
                "Zemlemoria",
                "Middle Earch",
                "The green Kingdom",
                "Westeros"
            };


            var validTowns = new List<Town>();
            var rnd = new Random();

            for (int i = 0; i < townNames.Length; i++)
            {


                int countryIndex = rnd.Next(0, countries.Length - 1);

                var town = new Town()
                {
                    Name = townNames[i],
                    Country = countries[countryIndex]

                };

                validTowns.Add(town);


            }

            context.AddRange(validTowns);
            context.SaveChanges();

        }

    }
}
