using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace XmlCars.Models
{
    public class Supplier
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public bool IsImporter { get; set; }

        public ICollection<Part> Parts { get; set; } = new List<Part>();

    }
}
