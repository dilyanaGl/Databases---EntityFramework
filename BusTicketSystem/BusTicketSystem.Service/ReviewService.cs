using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusTicketSystem.Data;
using BusTicketSystem.Models;
using BusTicketSystem.Service.Contracts;

namespace BusTicketSystem.Service
{
    public class ReviewService : IReviewService
    {
        private readonly BusTicketContext context;

        public ReviewService(BusTicketContext context)
        {
            this.context = context;

        }

        public void PublishReview(int customerId, double grade, int companyId, string content)
        {

            var review = new Review
            {

                CustomerId = customerId,
                Grade = grade,
                BusCompanyId = companyId,
                Content = content,
                DateOfPublishing = DateTime.Now
            };


            context.Reviews.Add(review);
            context.SaveChanges();

        }

        public string PrintReviews(int busCompanyId)
        {

            var reviews = context.Reviews.Where(p => p.BusCompanyId == busCompanyId)
                .Select(p => new
                {
                    Id = p.Id,
                    Grade = p.Grade,
                    DateTime = p.DateOfPublishing,
                    FullName = $"{p.Customer.FirstName} {p.Customer.LastName}",
                    Content = p.Content
                })
                .ToArray();

            var sb = new StringBuilder();

            foreach (var review in reviews)
            {

                sb.AppendLine($"{review.Id} {review.Grade} {review.DateTime} {review.FullName} {review.Content}");

            }

            return sb.ToString().Trim();

        }
    }
}
