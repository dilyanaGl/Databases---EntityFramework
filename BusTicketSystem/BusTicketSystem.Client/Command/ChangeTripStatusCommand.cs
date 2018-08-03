using System;
using System.Collections.Generic;
using System.Text;
using BusTicketSystem.Client.Contracts;
using BusTicketSystem.Service.Contracts;

namespace BusTicketSystem.Client.Command
{
    public class ChangeTripStatusCommand : ICommand
    {
        private const string TripNotFound = "Trip not found!";

        private readonly ITripService tripService;

        public ChangeTripStatusCommand(ITripService tripService)
        {
            this.tripService = tripService;
        }

        public string Execute(string[] data)
        {
            int tripId = int.Parse(data[0]);
            string status = data[1];

            if (!tripService.Exists(tripId))
            {

                return TripNotFound;
            }


            return tripService.ChangeTripStatus(tripId, status);

        }
    }
}
