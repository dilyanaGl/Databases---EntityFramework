using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusTicketSystem.Models
{
    public class BusCompany
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Nationality { get; set; }

        [Range(0d, 10d)]
        public double Rating { get; set; }

        public ICollection<Trip> Trips { get; set; } = new List<Trip>();

        public ICollection<Review> Reviews { get; set; } = new List<Review>();


    }
}
