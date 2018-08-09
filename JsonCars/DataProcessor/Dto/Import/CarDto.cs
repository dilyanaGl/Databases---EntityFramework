using System.ComponentModel.DataAnnotations;

namespace DataProcessor.Dto.Import
{
    public class CarDto
    {

        [Required]
        public string Make { get; set; }

        [Required]
        public string Model { get; set; }

        public long TravelledDistance { get; set; }
    }
}