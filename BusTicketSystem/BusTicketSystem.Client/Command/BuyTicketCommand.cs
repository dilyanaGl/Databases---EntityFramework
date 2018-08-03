using System;
using System.Collections.Generic;
using System.Text;
using BusTicketSystem.Client.Contracts;
using BusTicketSystem.Client.Dto;
using BusTicketSystem.Service.Contracts;

namespace BusTicketSystem.Client.Command
{
    public class BuyTicketCommand : ICommand
    {
        private const string CustomerNotFound = "Customer not found";
        private const string TripNotFound = "Trip not found";
        private const string CompanyNotFound = "Company not found";
        private const string InvalidArgs = "Invalid arguments";
        private const string NotEnoughMoney = "Not enough balance in bank account";


        private readonly ITicketService ticketService;
        private readonly ICustomerService customerService;
        private readonly ITripService tripService;

        public BuyTicketCommand(ITicketService ticketService, ICustomerService customerService, ITripService tripService)
        {
            this.ticketService = ticketService;
            this.customerService = customerService;
            this.tripService = tripService;
        }

        public string Execute(string[] data)
        {
            var ticketDto = new TicketDto
            {
                CustomerId = int.Parse(data[0]),
                TripId = int.Parse(data[1]),
                Price = decimal.Parse(data[2]),
                Seat = data[3]
            };


            if (!Validation.Validation.IsValid(ticketDto))
            {
                return InvalidArgs;
            }


            if (!customerService.Exists(ticketDto.CustomerId))
            {
                return CustomerNotFound;

            }

            if (!tripService.Exists(ticketDto.TripId))
            {
                return TripNotFound;

            }

            if (!customerService.HasEnoughMoney(ticketDto.CustomerId, ticketDto.Price))
            {
                return NotEnoughMoney;
            }

            customerService.PayTicket(ticketDto.CustomerId, ticketDto.Price);

            return ticketService.BuyTicket(ticketDto.CustomerId, ticketDto.TripId, ticketDto.Price, ticketDto.Seat);
        }

        
    }
}
