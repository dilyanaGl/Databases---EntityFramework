using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;

namespace PetClinic.DataProcessor.Inports
{
    [XmlType("Vet")]
    public class VetDto
    {
        [MinLength(3)]
        [MaxLength(40)]
        public string Name { get; set; }

        [MinLength(3)]
        [MaxLength(50)]
        public string Profession { get; set; }

        [Range(22, 65)]
        public int Age { get; set; }

        [Required]
        public DateTime DateTime { get; set; }

        [Required]
        [RegularExpression("(^[+]359[0-9]{9}$)|(^(0[0-9]{9})$)")]
        public string PhoneNumber { get; set; }
    }
}
