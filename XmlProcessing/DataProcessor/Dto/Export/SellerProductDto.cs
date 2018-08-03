using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace DataProcessor.Dto.Export
{
    [XmlType("sold-product")]
    public class SellerProductDto
    {
        [XmlAttribute("count")]
        public int Count { get; set; }

        [XmlElement("product")]
        public SellerSoldProductDto[] Products { get; set; }
    }
}
