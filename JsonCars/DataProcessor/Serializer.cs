using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataProcessor.Dto.Export;
using DataProcessor.Dto.Import;
using JsonProducts.App;
using Newtonsoft.Json;
using SupplierDto = DataProcessor.Dto.Export.SupplierDto;

namespace DataProcessor
{
    public class Serializer
    {
        public static string ExportOrderedCustomers(CarContext context)
        {
            var users = context.Customers.Select(p => new OrderedCustomerDto
                {

                    Id = p.Id,
                    Name = p.Name,
                    BirthDate = p.BirthDate,
                    IsYoungDriver = p.IsYoungDriver
                })
                .OrderBy(p => p.BirthDate)
                .ToArray();

            var jsonString = JsonConvert.SerializeObject(users, Formatting.Indented);

            return jsonString;


        }

        public static string ExportToyota(CarContext context)
        {

            var cars = context.Cars.Where(p => p.Make == "Toyota")
                .Select(p => new ToyotaDto
                {
                    Id = p.Id,
                    Make = p.Make,
                    Model = p.Model,
                    TravelledDistance = p.TravelledDistance
                })
                .OrderBy(p => p.Make)
                .ThenByDescending(p => p.TravelledDistance)
                .ToArray();


            var jsonString = JsonConvert.SerializeObject(cars, Formatting.Indented);

            return jsonString;
        }

        public static string ExportLocalSuppliers(CarContext context)
        {

            var localSuppliers = context.Suppliers.Where(p => p.IsImporter == false)
                .Select(p => new SupplierDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    PartsCount = p.Parts.Count
                })
                .ToArray();


            var jsonString = JsonConvert.SerializeObject(localSuppliers, Formatting.Indented);

            return jsonString;

        }

        public static string ExportCarsParts(CarContext context)
        {

            var carsParts = context.Cars.Select(p => new CarPartDto
            {
                Make = p.Make,
                Model = p.Model,
                TravelledDistance = p.TravelledDistance,
                Parts = p.Parts.Select(k => new PartCarDto
                {
                    Name = k.Part.Name,
                    Price = k.Part.Price
                })
                        .ToArray()
            })
                .ToArray();

            var jsonString = JsonConvert.SerializeObject(carsParts, Formatting.Indented);

            return jsonString;

        }

        public static string ExportSalesByCustomer(CarContext context)
        {

            var buyers = context.Customers.Where(p => p.Sales.Any())
                .Select(p => new BuyerDto
                {
                    FullName = p.Name,
                    BoughtCars = p.Sales.Count(),
                    SpentMoney = p.Sales.Sum(k => k.Car.Parts.Sum(l => l.Part.Price))
                })
                .OrderByDescending(p => p.SpentMoney)
                .ThenByDescending(p => p.BoughtCars)
                .ToArray();

            var jsonString = JsonConvert.SerializeObject(buyers, Formatting.Indented);

            return jsonString;


        }

        public static string ExportSalesWithAppliedDiscount(CarContext context)
        {

            var sales = context.Sales.Where(p => p.Discount > 0)
                .Select(p => new SaleDto
                {
                    Car = new CarPartDto
                    {
                        Make = p.Car.Make,
                        Model = p.Car.Model,
                        TravelledDistance = p.Car.TravelledDistance
                    },
                    CustomerName = $"{p.Customer.Name}",
                    Discount = p.Discount,
                    Price = p.Car.Parts.Select(k => k.Part.Price).Sum(),
                    PriceWithDiscount = p.Car.Parts.Select(k => k.Part.Price).Sum() * (1 - p.Discount)
                })

                .ToArray();


            var jsonString = JsonConvert.SerializeObject(sales, Formatting.Indented);

            return jsonString;
        }


    }
}
