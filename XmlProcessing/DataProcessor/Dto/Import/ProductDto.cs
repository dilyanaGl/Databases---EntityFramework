using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace DataProcessor.Dto.Import
{
    [XmlType("product")]
    public class ProductDto
    {
        [Required]
        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("price")]
        public decimal Price { get; set; }
    }
}