using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace DataProcessor.Dto.Import
{
    [XmlType("category")]
    public class CategoryDto
    {
        [Required]
        [XmlElement("name")]
        public string Name { get; set; }
    }
}