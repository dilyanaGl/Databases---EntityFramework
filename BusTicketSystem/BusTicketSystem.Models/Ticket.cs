using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BusTicketSystem.Models
{
    public class Ticket
    {
        public int Id { get; set; }

        [Required]
        public string Seat { get; set; }

        [Range(typeof(decimal), "0", "79228162514264337593543950335")]
        public decimal Price { get; set; }

        public int CustomerId { get; set; }

        [Required]
        public Customer Customer { get; set; }

        public int TripId { get; set; }

        [Required]
        public Trip Trip { get; set; }

    }
}
