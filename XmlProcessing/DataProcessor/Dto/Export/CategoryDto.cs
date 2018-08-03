using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace DataProcessor.Dto.Export
{
    [XmlType("category")]
    public class CategoryDto
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlElement("product-count")]
        public int ProductsCount { get; set; }

        [XmlElement("average-price")]
        public decimal AveragePrice { get; set; }

        [XmlElement("total-revenue")]
        public decimal TotalRevenue { get; set; }
    }
}
