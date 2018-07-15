using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using FastFood.Data;
using FastFood.DataProcessor.Dto.Export;
using FastFood.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace FastFood.DataProcessor
{
	public class Serializer
	{
		public static string ExportOrdersByEmployee(FastFoodDbContext context, string employeeName, string orderType)
		{
		    var employeeOrders = context.Orders
		        .Include(p => p.OrderItems)
		        .ThenInclude(k => k.Item)
		        .Where(p => p.Employee.Name == employeeName)
		        .Select(p => new
		        {
                    Customer = p.Customer, 
                    Items = p.OrderItems
                        .Select(k => new
                    {
                        Name = k.Item.Name, 
                        Price = k.Item.Price, 
                        Quantity= k.Quantity
                    }),
                    TotalPrice = p.OrderItems.Sum(k => k.Item.Price * k.Quantity)

		        })
		        .OrderByDescending(p => p.TotalPrice)
		        .ThenBy(p => p.Items.Count())
		        .ToArray();
		        
            var result =  new
            {
                Name = employeeName, 
                Orders = employeeOrders,
                TotalMade = employeeOrders.Sum(p => p.TotalPrice)

            };


		    var json = JsonConvert.SerializeObject(result, Newtonsoft.Json.Formatting.Indented);

		    return json;
		}

		public static string ExportCategoryStatistics(FastFoodDbContext context, string categoriesString)
		{
		    var sb = new StringBuilder();
		    var categories = categoriesString.Split(',').ToArray();
		    var list = new List<CategoryDto>();

		    foreach (var category in categories)
		    {
		        var item = context
		            .Items
		            .Include(p => p.OrderItems)
		            .Where(p => p.Category.Name == category)
		            .Select(p => new ItemExportDto()
		            {
		                Name = p.Name,
		                TotalMade = p.Price * p.OrderItems.Sum(k => k.Quantity),
		                TimesSold = p.OrderItems.Sum(l => l.Quantity)
		            })
		            .OrderByDescending(p => p.TotalMade)
		            .FirstOrDefault();
		        
               var cat = new CategoryDto()
               {
                   Name = category, 
                   MostPopularItem = item
               };

                list.Add(cat);
		    }

		    list = list.OrderByDescending(p => p.MostPopularItem.TotalMade).ThenByDescending(p => p.MostPopularItem.TimesSold).ToList();

		    var serializer = new XmlSerializer(typeof(List<CategoryDto>), new XmlRootAttribute("Categories"));

            serializer.Serialize(new StringWriter(sb), list, new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty }));

		    return sb.ToString();
		}
	}
}