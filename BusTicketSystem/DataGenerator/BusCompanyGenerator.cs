using System;
using System.Collections.Generic;
using System.Text;
using BusTicketSystem.Data;
using BusTicketSystem.Models;

namespace DataGenerator
{
    public static class BusCompanyGenerator
    {
        public static void GenerateCompaines(BusTicketContext context)
        {
            var busCompanyNames = new string[]{
                "UnionIvkoni",
                "UnionDimitrovi",
                "BussesAreUs",
                "DestinationFantasia"
            };


            var nationality = new string[]{
                "Bulgarian",
                "English",
                "French",
                "German",
                "Danish"

            };

            var rnd = new Random();

            var validCompanies = new List<BusCompany>();


            for (int i = 0; i < busCompanyNames.Length; i++)
            {

                var nationalityIndex = rnd.Next(0, nationality.Length - 1);
                double rating = rnd.Next(0, 10) / (double)rnd.Next(1, 5);

                var company = new BusCompany
                {
                    Name = busCompanyNames[i],
                    Nationality = nationality[nationalityIndex],
                    Rating = rating

                };

                validCompanies.Add(company);
            }

            context.AddRange(validCompanies);
            context.SaveChanges();

        }
    }
}
