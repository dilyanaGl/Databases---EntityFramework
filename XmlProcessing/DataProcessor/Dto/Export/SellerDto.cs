using System.Xml.Serialization;

namespace DataProcessor.Dto.Export
{
    [XmlType("user")]
    public class SellerDto
    {
        [XmlAttribute("first-name")]
        public string FirstName { get; set; }

        [XmlAttribute("last-name")]
        public string LastName { get; set; }

        [XmlArray("sold-products")]
        public SoldProductDto[] SoldProducts { get; set; }
    }
}
