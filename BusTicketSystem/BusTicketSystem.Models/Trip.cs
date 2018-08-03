using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using BusTicketSystem.Models.Enums;

namespace BusTicketSystem.Models
{
    public class Trip
    {
        public int Id { get; set; }

        [Required]
        public DateTime DepartureTime { get; set; }

        public DateTime ArrivalTime { get; set; }

        public Status Status { get; set; }

        public int OriginStationId { get; set; }

        [Required]
        public BusStation OriginStation { get; set; }

        public int DestinationStationId { get; set; }

        [Required]
        public BusStation DestinationStation { get; set; }

        public int BusCompanyId { get; set; }

        public BusCompany BusCompany { get; set; }

        public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();

       
    }
}
