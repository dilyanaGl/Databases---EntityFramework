using System.ComponentModel.DataAnnotations;

namespace DataProcessor.Dto.Import
{
    public class UserDto
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int? Age { get; set; }
    }
}