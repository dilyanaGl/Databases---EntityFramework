using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BusTicketSystem.Models
{
    public class BankAccount
    {
        public int Id { get; set; }

        [Required]
        public string AccountNumber { get; set; }

        [Range(typeof(decimal), "0", "79228162514264337593543950335")]
        public decimal Balance { get; set; }

        public int CustomerId { get; set; }

        [Required]
        public Customer Customer { get; set; }
    }
}
