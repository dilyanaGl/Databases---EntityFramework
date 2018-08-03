using System.Xml.Serialization;

namespace DataProcessor.Dto.Export
{
    [XmlType("cars")]
    public class FerrariDto
    {
        [XmlElement("id")]
        public int Id { get; set; }

        [XmlElement("model")]
        public string Model { get; set; }

        [XmlElement("travelled-distance")]
        public long TravelledDistance { get; set; }
    }
}