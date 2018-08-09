using System;
using System.IO;
using JsonProducts.App;

namespace JsonProducts.Client
{
   public class Program
    {
        public static void Main(string[] args)
        {
            var db = new CarContext();
            ResetDatabase(db);

            Console.WriteLine("Database reset...");

            ImportEntities(db);
            ExportEntities(db);
        }

        private static void ImportEntities(CarContext context, string baseDir = @"Data\")
        {

            var suppliersPath = $"{baseDir}suppliers.json";
            var supplierString = File.ReadAllText(suppliersPath);
            Console.WriteLine(DataProcessor.Deserializer.ImportSuppliers(context, supplierString));


            var partsPath = $"{baseDir}parts.json";
            var partString = File.ReadAllText(partsPath);
            Console.WriteLine(DataProcessor.Deserializer.ImportParts(context, partString));

            
            var carsPath = $"{baseDir}cars.json";
            var carString = File.ReadAllText(carsPath);
            Console.WriteLine(DataProcessor.Deserializer.ImportCars(context, carString));
            
            var customersPath = $"{baseDir}customers.json";
            var customerString = File.ReadAllText(customersPath);
            Console.WriteLine(DataProcessor.Deserializer.ImportCustomers(context, customerString));


            DataProcessor.Deserializer.ImportCarParts(context);
            DataProcessor.Deserializer.ImportSales(context);
        }

        public static void ExportEntities(CarContext context)
        {

            const string exportDir = @"ExportResults\";

            File.WriteAllText($"{exportDir}orderedCustomers.json", DataProcessor.Serializer.ExportOrderedCustomers(context));


            File.WriteAllText($"{exportDir}toyota.json", DataProcessor.Serializer.ExportToyota(context)
            );


            File.WriteAllText($"{exportDir}localSuppliers.json", DataProcessor.Serializer.ExportLocalSuppliers(context));


            File.WriteAllText($"{exportDir}exportCarParts.json", DataProcessor.Serializer.ExportCarsParts(context));

            File.WriteAllText($"{exportDir}exportSalesByCustomer.json",

                DataProcessor.Serializer.ExportSalesByCustomer(context));

            File.WriteAllText($"{exportDir}salesDiscount.json", DataProcessor.Serializer.ExportSalesWithAppliedDiscount(context));

        }

        private static void ResetDatabase(CarContext db)
        {
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

        }

    }
}
