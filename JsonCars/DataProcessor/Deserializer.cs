using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using DataProcessor.Dto.Import;
using JsonProduct.Models;
using JsonProducts.App;
using Newtonsoft.Json;

namespace DataProcessor
{
    public static class Deserializer
    {
        public const string FailureMessage = "Invalid data!";
        public const string SuccessMessage = "Successfully imported {0} {1}";

        public static string ImportSuppliers(CarContext context, string jsonString)
        {

            var sb = new StringBuilder();

            var deserialisedSuppliers = JsonConvert.DeserializeObject<SupplierDto[]>(jsonString);

            var validSuppliers = new List<Supplier>();

            foreach (var obj in deserialisedSuppliers)
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
                sb.AppendLine(String.Format(SuccessMessage, "category", obj.Name));

            }

            context.AddRange(validSuppliers);
            context.SaveChanges();


            return sb.ToString();
        }

        public static string ImportParts(CarContext context, string jsonString)
        {

            var sb = new StringBuilder();

            var deserialisedParts = JsonConvert.DeserializeObject<PartDto[]>(jsonString);

            var validParts = new List<Part>();
            var rnd = new Random();

            var suppliers = context.Suppliers.ToArray();

            foreach (var obj in deserialisedParts)
            {

                if (!IsValid(obj))
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                int index = rnd.Next(0, suppliers.Length - 1);

                var part = new Part
                {
                    Name = obj.Name,
                    Price = obj.Price,
                    Quantity = obj.Quantity,
                    Supplier = suppliers[index]
                };

                validParts.Add(part);

                sb.AppendLine(String.Format(SuccessMessage, "part", obj.Name));
            }

            context.AddRange(validParts);
            context.SaveChanges();

            return sb.ToString();
        }

        public static string ImportCars(CarContext context, string jsonString)
        {

            var sb = new StringBuilder();

            var deserialisedCars = JsonConvert.DeserializeObject<CarDto[]>(jsonString);

            var validCars = new List<Car>();

            foreach (var obj in deserialisedCars)
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
                sb.AppendLine(String.Format(SuccessMessage, obj.Make, obj.Model));

            }

            context.Cars.AddRange(validCars);
            context.SaveChanges();

            return sb.ToString();
        }

        public static string ImportCustomers(CarContext context, string jsonString)
        {

            var sb = new StringBuilder();

            var deserialisedCustomers = JsonConvert.DeserializeObject<CustomerDto[]>(jsonString);

            var validCustomers = new List<Customer>();

            foreach (var obj in deserialisedCustomers)
            {

                if (!IsValid(obj))
                {

                    sb.AppendLine(FailureMessage);
                    continue;

                }

                DateTime birthDate = DateTime.Parse(obj.BirthDate);

                var customer = new Customer
                {
                    Name = obj.Name,
                    BirthDate = birthDate,
                    IsYoungDriver = obj.IsYoungDriver

                };

                validCustomers.Add(customer);
                sb.AppendLine(String.Format(SuccessMessage, "customer", obj.Name));


            }
            context.AddRange(validCustomers);
            context.SaveChanges();

            return sb.ToString();
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

            for (int i = 0; i < 100; i++)
            {
                var index = rnd.Next(0, cars.Length - 1);
                var discountIndex = rnd.Next(0, discounts.Length - 1);
                var customerIndex = rnd.Next(0, customers.Length - 1);
                var sale = new Sale
                {
                    Customer = customers[customerIndex],
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


