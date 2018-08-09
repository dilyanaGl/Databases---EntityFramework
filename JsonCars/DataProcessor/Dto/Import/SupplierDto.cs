using System.ComponentModel.DataAnnotations;

namespace DataProcessor.Dto.Import
{
    public class SupplierDto
    {
        [Required]
        public string Name { get; set; }

        public bool IsImporter { get; set; }
    }
}