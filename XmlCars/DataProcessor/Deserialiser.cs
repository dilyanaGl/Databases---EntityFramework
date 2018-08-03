using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using DataProcessor.Dto.Import;
using XmlCars.Data;
using XmlCars.Models;

namespace DataProcessor
{
    public static class Deserialiser
    {
        private const string FailureMessage = "Invalid data!";
        private const string SuccessCarMessage = "Successfully imported {0} {1}";
        private const string SuccessCustomerMessage = "Successfully imported {0}";

        public static string ImportCars(CarContext context, string xmlString)
        {
            var sb = new StringBuilder();

            var serializer = new XmlSerializer(typeof(CarDto[]), new XmlRootAttribute("cars"));
            var deserializedCars = (CarDto[])serializer.Deserialize(new MemoryStream(Encoding.UTF8.GetBytes(xmlString)));


            var validCars = new List<Car>();

            foreach (var obj in deserializedCars)
            {
                if (!IsValid(obj))
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                var car = new Car
                {
                    Make = obj.Make,
                    Model = obj.Model,
                    TravelledDistance = obj.TravelledDistance
                };

                validCars.Add(car);


                sb.AppendLine(String.Format(SuccessCarMessage, obj.Make, obj.Model));
            }

            context.AddRange(validCars);
            context.SaveChanges();

            return sb.ToString().Trim();

        }


        public static string ImportCustomers(CarContext context, string xmlString)
        {
            var sb = new StringBuilder();

            var serializer = new XmlSerializer(typeof(CustomerDto[]), new XmlRootAttribute("customers"));
            var deserializedCustomers = (CustomerDto[])serializer.Deserialize(new MemoryStream(Encoding.UTF8.GetBytes(xmlString)));

            var validCustomers = new List<Customer>();

            foreach (var obj in deserializedCustomers)
            {

                if (!IsValid(obj))
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                DateTime birthDate;
                try
                {
                    birthDate = DateTime.Parse(obj.BirthDate);
                }
                catch (Exception)
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                var customer = new Customer
                {
                    Name = obj.Name,
                    BirthDate = birthDate,
                    IsYoungDriver = obj.IsYoungDriver
                };

                validCustomers.Add(customer);
                sb.AppendLine(String.Format(SuccessCustomerMessage, obj.Name));

            }


            context.AddRange(validCustomers);
            context.SaveChanges();

            return sb.ToString().Trim();

        }


        public static string ImportSuppliers(CarContext context, string xmlString)
        {

            var sb = new StringBuilder();

            var serializer = new XmlSerializer(typeof(SupplierDto[]), new XmlRootAttribute("suppliers"));
            var deserializedSuppliers = (SupplierDto[])serializer.Deserialize(new MemoryStream(Encoding.UTF8.GetBytes(xmlString)));

            var validSuppliers = new List<Supplier>();

            foreach (var obj in deserializedSuppliers)
            {
                if (!IsValid(obj))
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                var supplier = new Supplier
                {
                    Name = obj.Name,
                    IsImporter = obj.IsImporter
                };

                validSuppliers.Add(supplier);
                sb.AppendLine(String.Format(SuccessCustomerMessage, obj.Name));

            }

            context.AddRange(validSuppliers);
            context.SaveChanges();

            return sb.ToString().Trim();

        }

        public static string ImportParts(CarContext context, string xmlString)
        {
            var sb = new StringBuilder();

            var serializer = new XmlSerializer(typeof(PartDto[]), new XmlRootAttribute("parts"));
            var deserializedParts = (PartDto[])serializer.Deserialize(new MemoryStream(Encoding.UTF8.GetBytes(xmlString)));

            var suppliers = context.Suppliers.ToArray();
            var rnd = new Random();

            var validParts = new List<Part>();

            foreach (var obj in deserializedParts)
            {
                if (!IsValid(obj))
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                int index = rnd.Next(0, suppliers.Length - 1);

                var part = new Part()
                {
                    Name = obj.Name,
                    Price = obj.Price,
                    Quantity = obj.Quantity,
                    Supplier = suppliers[index]

                };

                validParts.Add(part);

                sb.AppendLine(String.Format(SuccessCustomerMessage, part.Name));

            }

            context.AddRange(validParts);
            context.SaveChanges();

            return sb.ToString().Trim();

        }

        public static void ImportCarParts(CarContext context)
        {

            var carParts = new List<PartCar>();

            var cars = context.Cars.ToArray();
            var parts = context.Parts.ToArray();

            var rnd = new Random();

            foreach (var car in cars)
            {
                int[] indexes = new int[10];

                for (int i = 0; i < 10; i++)
                {
                    while (true)
                    {
                        int index = rnd.Next(0, parts.Length - 1);
                        if (!indexes.Contains(index))
                        {
                            var carPart = new PartCar
                            {
                                Car = car, 
                                Part = parts[index] 
                            };

                            indexes[i] = index;
                            carParts.Add(carPart);

                            break;
                        }
                    }

                    

                }
            }

            context.AddRange(carParts);
            context.SaveChanges();
        }


        public static void ImportSales(CarContext context)
        {
            var discounts = new decimal[]
            {
                0m,
                0.5m,
                0.10m,
                0.15m,
                0.20m,
                0.30m,
                0.40m,
                0.50m
            };

            var rnd = new Random();

            var cars = context.Cars.ToArray();
            var customers = context.Customers.ToArray();
            var validSales = new List<Sale>();

            foreach (var customer in customers)
            {
                var index = rnd.Next(0, cars.Length - 1);
                var discountIndex = rnd.Next(0, discounts.Length - 1);

                var sale = new Sale
                {
                    Customer = customer,
                    Discount = discounts[discountIndex],
                    Car = cars[index]
                };

                validSales.Add(sale);
            }



            context.AddRange(validSales);
            context.SaveChanges();
        }

        private static bool IsValid(object obj)
        {
            var validationContext = new ValidationContext(obj);
            var validationResults = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(obj, validationContext, validationResults, true);
            return isValid;
        }
    }
}
