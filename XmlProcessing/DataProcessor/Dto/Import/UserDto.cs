using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace DataProcessor.Dto.Import
{
    [XmlType("user")]
    public class UserDto
    {
        [Required]
        [XmlAttribute("firstName")]
        public string FirstName { get; set; }

        [Required]
        [XmlAttribute("lastName")]
        public string LastName { get; set; }

        [XmlAttribute("age")]
        public int Age { get; set; }


    }
}
