using System;
using System.Collections.Generic;
using System.Text;
using FastFood.DataProcessor.Dto.Import;

namespace FastFood.DataProcessor.Dto.Export
{
    class EmployeeDto
    {
        public string Name { get; set; }

        public OrderExportDto[] Orders { get; set; }

        public decimal TotalMade { get; set; }
    }
}
