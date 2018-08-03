using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BusTicketSystem.Models
{
    public class BusStation
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int TownId { get; set; }

        [Required]
        public Town Town { get; set; }

        public ICollection<Trip> OriginTrips { get; set; } = new List<Trip>();

        public ICollection<Trip> DestinationTrips { get; set; } = new List<Trip>();

        public ICollection<ArrivedTrip> ArrivedOriginTrips { get; set; } = new List<ArrivedTrip>();

        public ICollection<ArrivedTrip> ArrivedDestinationTrips { get; set; } = new List<ArrivedTrip>();


    }
}
