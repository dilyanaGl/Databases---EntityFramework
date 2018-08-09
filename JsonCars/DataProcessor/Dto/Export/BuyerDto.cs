using System;
using System.Collections.Generic;
using System.Text;

namespace DataProcessor.Dto.Export
{
    public class BuyerDto
    {
        public string FullName { get; set; }

        public int BoughtCars { get; set; }

        public decimal SpentMoney { get; set; }
    }
}
