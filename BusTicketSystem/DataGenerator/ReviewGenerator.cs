using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusTicketSystem.Data;
using BusTicketSystem.Models;

namespace DataGenerator
{
    public static class ReviewGenerator
    {
        public static void GenerateReview(BusTicketContext context)
        {
            var content = new string[]{
                "good",
                "bad",
                "very good",
                "very bad",
                "Excellent",
                "Appaling",
                "Shockingly bad"

            };


            var validReviews = new List<Review>();

            var companyIds = context.BusCompanies.Select(p => p.Id).ToArray();
            var customerIds = context.Customers.Select(p => p.Id).ToArray();

            var rnd = new Random();

            for (int i = 0; i < 5; i++)
            {

                var reviewIndex = rnd.Next(0, content.Length - 1);
                var companyIndex = rnd.Next(0, companyIds.Length - 1);
                var customerIndex = rnd.Next(0, customerIds.Length - 1);
                var grade = rnd.Next(0, 10) / rnd.Next(1, 5);

                var review = new Review
                {
                    Content = content[reviewIndex],
                    Grade = grade,
                    CustomerId = customerIds[customerIndex],
                    DateOfPublishing = DateTime.Now,
                    BusCompanyId = companyIds[companyIndex] 

                };

                validReviews.Add(review);

            }

            context.AddRange(validReviews);
            context.SaveChanges();
        }
    }
}
