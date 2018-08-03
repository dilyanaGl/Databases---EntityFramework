using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BusTicketSystem.Models
{
    public class Review
    {
        public int Id { get; set; }

        [Required]
        public string Content { get; set; }

        [Range(0d, 10d)]
        public double Grade { get; set; }

        public int BusCompanyId { get; set; }

        public BusCompany BusCompany { get; set; }
        
        public int CustomerId { get; set; }

        [Required]
        public Customer Customer { get; set; }

        public DateTime DateOfPublishing { get; set; }
    }
}
