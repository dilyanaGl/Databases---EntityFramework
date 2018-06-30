using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace PetClinic.DataProcessor.Inports
{
    [XmlType("AnimalAid")]
    public class AnimalAidNameDto
    {
        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        public string Name { get; set; }

    }
}