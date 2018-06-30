using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace PetClinic.DataProcessor.Exports
{
    [XmlType("AnimalAid")]
    public class AnimalAidDto
    {
        public string Name { get; set; }

        public decimal Price { get; set; }
    }
}
