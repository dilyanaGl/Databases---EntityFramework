using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;


namespace FastFood.DataProcessor.Dto.Import
{
    class ItemDto
    {

        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        public string Name { get; set; }

        [Required]
        [Range(0.01d, (double)Decimal.MaxValue)]
        public decimal Price { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        public string Category { get; set; }

    }
}
