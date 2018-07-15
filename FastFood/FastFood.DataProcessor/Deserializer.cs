using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using FastFood.Data;
using FastFood.DataProcessor.Dto.Export;
using FastFood.DataProcessor.Dto.Import;
using FastFood.Models;
using FastFood.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using EmployeeDto = FastFood.DataProcessor.Dto.Import.EmployeeDto;

namespace FastFood.DataProcessor
{
	public static class Deserializer
	{
		private const string FailureMessage = "Invalid data format.";
		private const string SuccessMessage = "Record {0} successfully imported.";
	    private const string SuccessOrder = "Order for {0} on {1} added";


        public static string ImportEmployees(FastFoodDbContext context, string jsonString)
		{
		    var sb = new StringBuilder();

		    var deserialisedEmployees = JsonConvert.DeserializeObject<EmployeeDto[]>(jsonString);

		    var validEmployees = new List<Employee>();

            foreach (var deserialised in deserialisedEmployees)
            {
                if (!IsValid(deserialised))
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                var position = context.Positions.Where(p => p.Name == deserialised.Position).FirstOrDefault();

                if (position == null)
                {
                    position = new Position()
                    {
                        Name = deserialised.Position
                    };
                    context.Positions.Add(position);
                    context.SaveChanges();
                }

                var employee = new Employee()
                {
                    Name = deserialised.Name,
                    Age = deserialised.Age,
                    Position = position
                };

                validEmployees.Add(employee);

                sb.AppendLine(String.Format(SuccessMessage, deserialised.Name));
            }

            context.AddRange(validEmployees);
		    context.SaveChanges();

		    return sb.ToString().Trim();
		}

	    public static string ImportItems(FastFoodDbContext context, string jsonString)
	    {
	        var sb = new StringBuilder();

	        var deserialisedObjects = JsonConvert.DeserializeObject<ItemDto[]>(jsonString);

            var validItems = new List<Item>();

            foreach (var obj in deserialisedObjects)
            {
                if (!IsValid(obj) || validItems.Any(p => p.Name == obj.Name))
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

              var category = context.Categories.SingleOrDefault(p => p.Name == obj.Category);

                if (category == null)
                {
                    category = new Category()
                    {
                        Name = obj.Category
                    };

                    context.Categories.Add(category);

                    context.SaveChanges();
                }

                var item = new Item()
                {
                    Name = obj.Name,
                    Price = obj.Price,
                    Category = category
                };

                validItems.Add(item);
                sb.AppendLine(String.Format(SuccessMessage, item.Name));
            }

            context.AddRange(validItems);
	        context.SaveChanges();

            return sb.ToString().Trim();
	    }

		public static string ImportOrders(FastFoodDbContext context, string xmlString)
		{
		    var sb = new StringBuilder();

		    var serialiser = new XmlSerializer(typeof(OrderDto[]), new XmlRootAttribute("Orders"));

		    var deserialisedOrders = (OrderDto[]) serialiser.Deserialize(new MemoryStream(Encoding.UTF8.GetBytes(xmlString)));

		    var validOrders = new List<Order>();
		    var orderItems = new List<OrderItem>();

            foreach (var obj in deserialisedOrders)
            {
                if (!IsValid(obj))
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                var isItemInvalid = false;
                var items = new Dictionary<Item, int>();
                    

                foreach (var itemImportDto in obj.Items)
                {
                    if (!IsValid(itemImportDto))
                    {
                        sb.AppendLine(FailureMessage);
                        isItemInvalid = true;
                    }

                    var item = context.Items.FirstOrDefault(p => p.Name == itemImportDto.Name);
                    if (item == null)
                    {
                        isItemInvalid = true;
                    }
                    else
                    {
                        items.Add(item, itemImportDto.Quantity);
                    }
                }

                if (isItemInvalid)
                {
                    continue;
                }

                var employee = context.Employees.FirstOrDefault(p => p.Name == obj.Employee);
                if (employee == null)
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                OrderType type;
                DateTime date;

                try
                {
                    type = Enum.Parse<OrderType>(obj.Type);
                    date = DateTime.ParseExact(obj.DateTime, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
                }
                catch
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                var order = new Order
                {
                    Customer = obj.Customer,
                    Type = type,
                    DateTime = date,
                    Employee = employee,
                    
                };

                validOrders.Add(order);

                foreach (var item in items)
                {
                    var orderItem = new OrderItem()
                    {
                        Order = order,
                        Item = item.Key, 
                        Quantity = item.Value
                    };

                    orderItems.Add(orderItem);
                }

                sb.AppendLine(String.Format(SuccessOrder, obj.Customer, obj.DateTime));


            }

            context.Orders.AddRange(validOrders);
            context.OrderItems.AddRange(orderItems);
		    context.SaveChanges();
            return sb.ToString().Trim();
		}

	    private static bool IsValid(object obj)
	    {
	        var validContext = new ValidationContext(obj);
	        var validationResults = new List<ValidationResult>();

	        var isValid = Validator.TryValidateObject(obj, validContext, validationResults, true);
	        return isValid;
	    }
	}
}