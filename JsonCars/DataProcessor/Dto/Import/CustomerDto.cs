using System.ComponentModel.DataAnnotations;

namespace DataProcessor.Dto.Import
{
    public class CustomerDto
    {
        [Required]
        public string Name { get; set; }

        public string BirthDate { get; set; }

        public bool IsYoungDriver { get; set; }
    }
}