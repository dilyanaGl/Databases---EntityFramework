using System.ComponentModel.DataAnnotations;

namespace DataProcessor.Dto.Import
{
    public class CategoryDto
    {
        [Required]
        public string Name { get; set; }
    }
}