using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JsonProcessing.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public decimal Price { get; set; }

        public int? BuyerId { get; set; }

        public User Buyer { get; set; }

        public int SellerId { get; set; }

        [Required]
        public User Seller { get; set; }

        public ICollection<CategoryProduct> Categories{ get; set; } = new List<CategoryProduct>();


       
    }
}
