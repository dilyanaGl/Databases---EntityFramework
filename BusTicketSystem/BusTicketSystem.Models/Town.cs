using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BusTicketSystem.Models
{
    public class Town
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Country { get; set; }

        public ICollection<BusStation> BusStations { get; set; } = new List<BusStation>();

        public ICollection<Customer> Citizens { get; set; } = new List<Customer>();

    }
}
