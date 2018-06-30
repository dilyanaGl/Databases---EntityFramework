using System;
using System.Xml.Serialization;

namespace PetClinic.DataProcessor.Exports
{
    [XmlType("Procedure")]
    [Serializable]
    public class ProcedureDto
    {
        public string Passport { get; set; }

        public string OwnerNumber { get; set; }

        public string DateTime { get; set; }

        [XmlIgnoreAttribute]
        public DateTime DateTimeComparison { get; set; }

        public AnimalAidDto[] AnimalAids { get; set; }

        public decimal TotalPrice { get; set; }


    }
}
