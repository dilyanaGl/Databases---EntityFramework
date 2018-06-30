using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;
using PetClinic.Models;

namespace PetClinic.DataProcessor.Inports
{
    [XmlType("Procedure")]
    public class ProcedureDto
    {
        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        public string Animal { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(40)]
        public string Vet { get; set; }

        [Required]
        public string DateTime { get; set; }

        public List<AnimalAidNameDto> AnimalAids{ get; set; }
    }
}
