using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace XmlCars.Models
{
    public class Part
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public int SupplierId { get; set; }

        public Supplier Supplier { get; set; }

        public ICollection<PartCar> Cars { get; set; } = new List<PartCar>();
    }
}
