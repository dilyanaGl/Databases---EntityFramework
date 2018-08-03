using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper.QueryableExtensions;
using BusTicketSystem.Data;
using BusTicketSystem.Models;
using BusTicketSystem.Models.Enums;
using BusTicketSystem.Service.Contracts;

namespace BusTicketSystem.Service
{
    public class TripService : ITripService
    {
       
        private readonly BusTicketContext context;

        public TripService(BusTicketContext context)
        {

            this.context = context;

        }

        public TModel ById<TModel>(int id)
            => By<TModel>(p => p.Id == id).SingleOrDefault();

        public bool Exists(int id)
            => ById<Trip>(id) != null;



        public string ChangeTripStatus(int id, string statusString)
        {
            var trip = context.Trips.SingleOrDefault(p => p.Id == id);
            var oldStatus = trip.Status.ToString();
            Status status;
            string originStationName = context.BusStations.SingleOrDefault(p => p.Id == trip.OriginStationId).Name;
            string destinationStationName =
                context.BusStations.SingleOrDefault(p => p.Id == trip.DestinationStationId).Name;
            try
            {
                status = Enum.Parse<Status>(statusString);
            }

            catch (Exception ex)
            {

                return "Invalid status";
            }


            trip.Status = status;
            context.SaveChanges();

            if (status == Status.Arrived)
            {

                var arrivedTrip = new ArrivedTrip()
                {
                    ArrivedTime = DateTime.Now, 
                    OriginStationId = trip.OriginStationId,
                    DestinationStationId = trip.DestinationStationId,
                    PassengersCount = context.Tickets.Count(p => p.TripId == trip.Id)
                };

                context.ArrivedTrips.Add(arrivedTrip);
                context.SaveChanges();
            }

            return $"Trip from {trip.OriginStation.Name} to {trip.DestinationStation.Name} on {trip.DepartureTime} Status changed from {oldStatus} to {status} ";
        }

        private IEnumerable<TModel> By<TModel>(Func<Ticket, bool> predicate)
            => this.context
                .Tickets
                .Where(predicate)
                .AsQueryable()
                .ProjectTo<TModel>();


    }
}



