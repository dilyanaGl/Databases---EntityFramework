using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using DataProcessor.Dto.Export;
using XmlCars.Data;

namespace DataProcessor
{
    public static class Serialiser
    {
        public static string ExportCars(CarContext context)
        {
            var cars = context.Cars.Where(p => p.TravelledDistance >= 2000000)
                .Select(p => new CarDto
                {

                    Make = p.Make,
                    Model = p.Model,
                    TravelledDistance = p.TravelledDistance
                })
                .OrderBy(p => p.Make)
                .ThenBy(p => p.Model)
                .ToArray();


            var serializer = new XmlSerializer(typeof(CarDto[]), new XmlRootAttribute("Cars"));

            var sb = new StringBuilder();

            serializer.Serialize(new StringWriter(sb),
                cars,
                new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty }));

            var result = sb.ToString();
            return result;

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

            var serializer = new XmlSerializer(typeof(SupplierDto[]), new XmlRootAttribute("suppliers"));
            var sb = new StringBuilder();

            serializer.Serialize(new StringWriter(sb),
                localSuppliers,
                new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty }));

            var result = sb.ToString();
            return result;
        }

        public static string ExportFerrari(CarContext context)
        {
            var cars = context.Cars.Where(p => p.Make == "Ferrari")
                .Select(p => new FerrariDto
                {

                    Id = p.Id,
                    Model = p.Model,
                    TravelledDistance = p.TravelledDistance
                })
                .OrderBy(p => p.Model)
                .ThenByDescending(p => p.TravelledDistance)
                .ToArray();

            var serializer = new XmlSerializer(typeof(FerrariDto[]), new XmlRootAttribute("Cars"));

            var sb = new StringBuilder();

            serializer.Serialize(new StringWriter(sb),
                cars,
                new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty }));

            var result = sb.ToString();
            return result;

        }

        public static string ExportCarParts(CarContext context)
        {
            var carsParts = context.Cars.Select(p => 
                    new CarPartDto {
                Make = p.Make,
                Model = p.Model,
                TravelledDistance = p.TravelledDistance,
                Parts = p.Parts.Select(k => new PartDto
                {
                    Name = k.Part.Name,
                    Price = k.Part.Price
                }).ToArray()
            })
                .ToArray();


            var serializer = new XmlSerializer(typeof(CarPartDto[]), new XmlRootAttribute("cars"));

            var sb = new StringBuilder();
            serializer.Serialize(new StringWriter(sb),
                carsParts,
                new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty }));

            var result = sb.ToString();
            return result;
        }

        public static string ExportCustomersWithSales(CarContext context)
        {
            var customers = context.Customers.Where(p => p.Sales.Count >= 1)
                .Select(p => new CustomerDto
                {
                    FullName = p.Name,
                    BoughtCars = p.Sales.Count(),
                    SpentMoney = p.Sales.Select(l => l.Car.Parts.Sum(k => k.Part.Price)).Sum()
                })
                .OrderByDescending(p => p.SpentMoney)
                .ThenByDescending(p => p.BoughtCars)
                .ToArray();

            var serializer = new XmlSerializer(typeof(CustomerDto[]), new XmlRootAttribute("Customers"));
            var sb = new StringBuilder();
            serializer.Serialize(new StringWriter(sb),
                customers,
                new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty }));


            var result = sb.ToString();
            return result;
        }

        public static string ExportSales(CarContext context)
        {
            var sales = context.Sales.Where(p => p.Discount != null && p.Discount > 0)
                .Select(p => new SaleDto
                {
                    Car = new CarPartDto
                    {
                        Make = p.Car.Make,
                        Model = p.Car.Model,
                        TravelledDistance = p.Car.TravelledDistance
                    },
                    Name = p.Customer.Name,
                    Discount = p.Discount,
                    Price = p.Car.Parts.Select(k => k.Part.Price).Sum(),
                    PriceWithDiscount = p.Car.Parts.Select(k => k.Part.Price).Sum() * (1 - p.Discount)
                })
                .ToArray();



            var serializer = new XmlSerializer(typeof(SaleDto[]), new XmlRootAttribute("Sales"));

            var sb = new StringBuilder();
            serializer.Serialize(new StringWriter(sb),
                sales,
                new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty }));

            var result = sb.ToString();
            return result;

        }
    }
}



