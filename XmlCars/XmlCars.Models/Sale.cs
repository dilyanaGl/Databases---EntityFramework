using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace XmlCars.Models
{
    public class Sale
    {
        public int Id { get; set; }

        public decimal Discount { get; set; }

        public int CarId { get; set; }

        [Required]
        public Car Car { get; set; }

        public int CustomerId { get; set; }

        [Required]
        public Customer Customer { get; set; }
    }
}
