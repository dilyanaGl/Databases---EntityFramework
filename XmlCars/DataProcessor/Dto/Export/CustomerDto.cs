using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace DataProcessor.Dto.Export
{
    [XmlType("customers")]
    public class CustomerDto
    {
        [XmlAttribute("full-name")]
        public string FullName { get; set; }

        [XmlAttribute("bought-cars")]
        public int BoughtCars { get; set; }

        [XmlAttribute("spent-money")]
        public decimal SpentMoney { get; set; }
    }
}
