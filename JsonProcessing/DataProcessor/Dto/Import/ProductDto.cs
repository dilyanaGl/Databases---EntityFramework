using System.ComponentModel.DataAnnotations;

namespace DataProcessor.Dto.Import
{
    public class ProductDto
    {
        [Required]
        public string Name { get; set; }

        [Range(typeof(decimal), "0", "79228162514264337593543950335")]
        public decimal Price { get; set; }
    }
}