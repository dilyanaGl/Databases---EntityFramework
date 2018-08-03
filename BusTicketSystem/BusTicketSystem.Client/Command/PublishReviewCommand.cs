using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusTicketSystem.Client.Contracts;
using BusTicketSystem.Models;
using BusTicketSystem.Service.Contracts;

namespace BusTicketSystem.Client.Command
{
    public class PublishReviewCommand : ICommand
    {
        private const string SuccessMessage = "Customer {0} {1} published review for company {2}";
        private const string CustomerNotFound = "Customer not found!";
        private const string CompanyNotFound = "Company not found!";


        private readonly ICustomerService userService;
        private readonly IReviewService reviewService;
        private readonly ICompanyService companyService;


        public PublishReviewCommand(ICustomerService userService, IReviewService reviewService,
            ICompanyService companyService)
        {
            this.userService = userService;
            this.reviewService = reviewService;
            this.companyService = companyService;
        }

        public string Execute(string[] data)
        {

            int customerId = int.Parse(data[0]);
            double grade = double.Parse(data[1]);
            string companyName = data[2];
            string content = String.Join(' ', data.Skip(3).ToArray());

            var customer = userService.ById<Customer>(customerId);
            var company = companyService.ByName<BusCompany>(companyName);

            if (customer == null)
            {
                return CustomerNotFound;

            }

            if (company == null)
            {

                return CompanyNotFound;
            }

            try
            {

                reviewService.PublishReview(customerId, grade, company.Id, content);
            }
            catch (Exception ex)
            {
                return ex.Message;

            }
            return string.Format(SuccessMessage, customer.FirstName, customer.LastName, companyName);
        }
    }
}
