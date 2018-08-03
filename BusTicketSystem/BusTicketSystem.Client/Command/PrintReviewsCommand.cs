using System;
using System.Collections.Generic;
using System.Text;
using BusTicketSystem.Client.Contracts;
using BusTicketSystem.Service.Contracts;

namespace BusTicketSystem.Client.Command
{
    public class PrintReviewsCommand : ICommand
    {
        private const string CompanyNotFound = "Company not found!";

        private readonly IReviewService reviewService;
        private readonly ICompanyService companyService;


        public PrintReviewsCommand(IReviewService reviewService, ICompanyService companyService)
        {
            this.reviewService = reviewService;
            this.companyService = companyService;
        }


        public string Execute(string[] data)
        {

            int busCompanyId = int.Parse(data[0]);

            if (!companyService.Exists(busCompanyId))
            {

                return CompanyNotFound;

            }

            return reviewService.PrintReviews(busCompanyId);
        }
    }
}
