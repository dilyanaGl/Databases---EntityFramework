using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace JsonProcessing.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public int Age { get; set; }

        public ICollection<Product> ProductsBought { get; set; } = new List<Product>();

        public ICollection<Product> ProductsSold { get; set; } = new List<Product>();
    }
}
