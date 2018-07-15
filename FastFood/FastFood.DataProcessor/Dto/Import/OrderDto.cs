using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;
using FastFood.Models;

namespace FastFood.DataProcessor.Dto.Import
{
    [XmlType("Order")]
   public  class OrderDto
    {
        [Required]
        public string Customer { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        public string Employee { get; set; }

        [Required]
        public string DateTime { get; set; }

        [Required]
        public string Type { get; set; }

        public List<ItemImportDto> Items { get; set; }
    }
}
