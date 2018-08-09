using System;
using System.Collections.Generic;
using System.Text;

namespace JsonProcessing.Models
{
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<CategoryProduct> Products { get; set; } = new List<CategoryProduct>();

    }
}
