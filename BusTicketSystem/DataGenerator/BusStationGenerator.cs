using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusTicketSystem.Data;
using BusTicketSystem.Models;

namespace DataGenerator
{
    public static class BusStationGenerator
    {
        public static void GenerateBusStations(BusTicketContext context)
        {
            var names = new string[] {

                "North Station",
                "West Station",
                "South Station",
                "East Station",
                "NorthEast Station",
                "NorthWest Station",
                "Central Station"

            };

            var townIds = context.Towns.Select(p => p.Id).ToArray();

            var validBusStations = new List<BusStation>();

            var rnd = new Random();

            for (int i = 0; i < names.Length; i++)
            {
                var townsIndex = rnd.Next(0, townIds.Length - 1);

                var busStation = new BusStation
                {

                    Name = names[i],
                    TownId = townIds[townsIndex]

                };

                validBusStations.Add(busStation);
            }

            context.AddRange(validBusStations);
            context.SaveChanges();

        
    }

    }
}
