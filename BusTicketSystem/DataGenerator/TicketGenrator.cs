using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusTicketSystem.Data;
using BusTicketSystem.Models;

namespace DataGenerator
{
    public static class TicketGenrator
    {
        public static void GenerateTickets(BusTicketContext context)
        {
            var seats = new string[]{

                "A1",
                "A2",
                "A3",
                "A4",
                "A5",
                "B1",
                "B2",
                "B3",
                "B4",
                "B5"
            };

            var customerIds = context.Customers.Select(p => p.Id).ToArray();
            var tripIds = context.Trips.Select(p => p.Id).ToArray();

            var rnd = new Random();

            var validTickets = new List<Ticket>();

            for (int i = 0; i < seats.Length; i++)
            {

                var customerIndex = rnd.Next(0, customerIds.Length - 1);
                var tripIndex = rnd.Next(0, tripIds.Length - 1);
                decimal price = rnd.Next(10, 20) / (decimal)rnd.Next(1, 5);

                var ticket = new Ticket
                {

                    Seat = seats[i],
                    CustomerId = customerIds[customerIndex],
                    TripId = tripIds[tripIndex],
                    Price = price

                };

                validTickets.Add(ticket);

            }

            context.AddRange(validTickets);
            context.SaveChanges();

        }
    }
}
