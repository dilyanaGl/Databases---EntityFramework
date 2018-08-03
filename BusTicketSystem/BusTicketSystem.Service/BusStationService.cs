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
    public class BusStationService : IBusStationService
    {
        private readonly BusTicketContext context;

        public BusStationService(BusTicketContext context)
        {
            this.context = context;
        }

        public TModel ById<TModel>(int id)
            => By<TModel>(p => p.Id == id).SingleOrDefault();

        public bool Exist(int id)
            =>  ById<BusStation>(id) != null;


        private IEnumerable<TModel> By<TModel>(Func<BusStation, bool> predicate)
            => this.context
                .BusStations
                .Where(predicate)
                .AsQueryable()
                .ProjectTo<TModel>();


        public string PrintInformation(int busStationId)
        {

            var busStation = context.BusStations.Where(p => p.Id == busStationId)
                .Select(p => new
                {
                    Name = p.Name,
                    Town = p.Town.Name,
                    Arrivals = p.OriginTrips.Select(k => new
                    {
                        StationName = k.OriginStation.Name,
                        Time = k.ArrivalTime,
                        Status = k.Status.ToString()
                    }),
                    Departures = p.DestinationTrips.Select(k => new
                    {
                        StationName = k.DestinationStation.Name,
                        Time = k.DepartureTime,
                        Status = k.Status.ToString()
                    })
                })
                .SingleOrDefault();



            var sb = new StringBuilder();

            sb.AppendLine($"{busStation.Name}, {busStation.Town}{Environment.NewLine}Arrivals:");


            foreach (var arrival in busStation.Arrivals)
            {
                sb.AppendLine($"From {arrival.StationName} | Arrive at: {arrival.Time} | Status: {arrival.Status}");
            }


            sb.AppendLine("Departures:");

            foreach (var departure in busStation.Departures)
            {
                sb.AppendLine("To {departure.StationName} | Arrive at: {departure.Time} | Status: {departure.Status}");
            }


            return sb.ToString().Trim();
        }
    }
}
