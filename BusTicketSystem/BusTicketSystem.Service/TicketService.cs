using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper.QueryableExtensions;
using BusTicketSystem.Data;
using BusTicketSystem.Models;
using BusTicketSystem.Service.Contracts;

namespace BusTicketSystem.Service
{
    public class TicketService : ITicketService
    {
        private readonly BusTicketContext context;

        public TicketService(BusTicketContext context)
        {

            this.context = context;

        }

        public TModel ById<TModel>(int id)
            => By<TModel>(p => p.Id == id).SingleOrDefault();
        
        public bool Exists(int id)
            => ById<Ticket>(id) != null;


        public string BuyTicket(int customerId, int tripId, decimal price, string seat)
        {

            var trip = context.Trips.SingleOrDefault(p => p.Id == tripId);
            var customer = context.Customers.SingleOrDefault(p => p.Id == customerId);
            


            var ticket = new Ticket
            {
                Trip = trip,
                Customer = customer,
                Seat = seat,
                Price = price

            };

            context.Tickets.Add(ticket);
            context.SaveChanges();

            return $"Customer {customer.FirstName} {customer.LastName} bought ticket for trip {tripId} for {price} on seat {seat}";

        }

        private IEnumerable<TModel> By<TModel>(Func<Ticket, bool> predicate)
            => this.context
                .Tickets
                .Where(predicate)
                .AsQueryable()
                .ProjectTo<TModel>();

    }
}
