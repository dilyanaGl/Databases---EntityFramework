using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace DataProcessor.Dto.Import
{
    [XmlType("car")]
    public class CarDto
    {
        [Required]
        [XmlElement("make")]
        public string Make { get; set; }

        [Required]
        [XmlElement("model")]
        public string Model { get; set; }

        [XmlElement("travelled-distance")]
        public long TravelledDistance { get; set; }
    }
}