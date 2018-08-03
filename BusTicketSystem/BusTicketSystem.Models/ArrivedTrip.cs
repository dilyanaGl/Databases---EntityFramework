using System;
using System.Collections.Generic;
using System.Text;

namespace BusTicketSystem.Models
{
    public class ArrivedTrip
    {
        public int Id { get; set; }

        public DateTime ArrivedTime { get; set; }

        public int OriginStationId { get; set; }

        public BusStation OriginStation { get; set; }

        public int DestinationStationId { get; set; }

        public BusStation DestinationStation { get; set; }

        public int PassengersCount{ get; set; }
    }
}
