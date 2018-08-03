using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusTicketSystem.Data;
using BusTicketSystem.Models;
using BusTicketSystem.Models.Enums;

namespace DataGenerator
{
    public static class TripGenerator
    {
        public static void GenerateTrips(BusTicketContext context)
        {
            var rnd = new Random();

            var stationIds = context.BusStations.Select(p => p.Id).ToArray();

            var companyIds = context.BusCompanies.Select(p => p.Id).ToArray();

            var validTrips = new List<Trip>();


            for (int i = 0; i < 10; i++)
            {

                int indexStation = rnd.Next(0, stationIds.Length - 1);
                int destinationIndex = rnd.Next(0, stationIds.Length - 1);
                while (indexStation == destinationIndex)
                {

                    destinationIndex = rnd.Next(0, stationIds.Length - 1);

                }

                int companyIndex = rnd.Next(0, companyIds.Length - 1);

                int days = rnd.Next(20, 100);

                var trip = new Trip
                {
                    DepartureTime = DateTime.Now.AddDays(days * -1),
                    ArrivalTime = DateTime.Now.AddDays(rnd.Next(1, 20)),
                    OriginStationId = stationIds[indexStation],
                    DestinationStationId = stationIds[destinationIndex],
                    BusCompanyId = companyIds[companyIndex],
                    Status = (Status)(i % 4)
                };

                validTrips.Add(trip);

            }

            context.AddRange(validTrips);
            context.SaveChanges();
        }
    }
}
