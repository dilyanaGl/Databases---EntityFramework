using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace DataProcessor.Dto.Import
{
    [XmlType("customer")]
    public class CustomerDto
    {
        [Required]
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlElement("birth-date")]
        public string BirthDate { get; set; }

        [XmlElement("is-young-driver")]
        public bool IsYoungDriver { get; set; }
    }
}