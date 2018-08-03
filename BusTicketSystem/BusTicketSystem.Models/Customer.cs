using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using BusTicketSystem.Models.Enums;

namespace BusTicketSystem.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public DateTime BirthDate { get; set; }

        public Gender Gender { get; set; }

        public int? HomeTownId { get; set; }

        public Town HomeTown { get; set; }

        public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();

        public ICollection<Review> Reviews { get; set; } = new List<Review>();

        public int BankAccountId { get; set; }

        public BankAccount BankAccount { get; set; }

    }
}
