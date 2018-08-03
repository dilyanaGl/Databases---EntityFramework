using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace DataProcessor.Dto.Export
{
    [XmlRoot("users")]
    public class UserProductDto
    {
        [XmlAttribute("count")]
        public int Count { get; set; }

        public UserDto[] Users { get; set; }
    }
}
