using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace DataProcessor.Dto.Export
{
    [XmlType("sale")]
    public class SaleDto
    {
        [XmlElement("car")]
        public CarPartDto Car { get; set; }

        [XmlElement("customer-name")]
        public string Name { get; set; }

        [XmlElement("discount")]
        public decimal Discount { get; set; }

        [XmlElement("price")]
        public decimal Price { get; set; }

        [XmlElement("price-with-discount")]
        public decimal PriceWithDiscount { get; set; }
    }
}
