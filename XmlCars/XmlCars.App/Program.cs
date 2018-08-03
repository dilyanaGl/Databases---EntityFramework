using System;
using System.IO;
using XmlCars.Data;

namespace XmlCars.App
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new CarContext();
            ResetDatabase(context);

            Console.WriteLine("Database Reset.");

            ImportEntities(context);
            ExportEntities(context);
        }

        private static void ImportEntities(CarContext context, string baseDir = @"Data\")
        {
            string supplierXml = File.ReadAllText(baseDir + "suppliers.xml");
            Console.WriteLine(DataProcessor.Deserialiser.ImportSuppliers(context, supplierXml));

            string partsXml = File.ReadAllText(baseDir + "parts.xml");
            Console.WriteLine(DataProcessor.Deserialiser.ImportParts(context, partsXml));

            string carsXml = File.ReadAllText(baseDir + "cars.xml");
            Console.WriteLine(DataProcessor.Deserialiser.ImportCars(context, carsXml));

            DataProcessor.Deserialiser.ImportCarParts(context);

            string customersXml = File.ReadAllText(baseDir + "customers.xml");
            Console.WriteLine(DataProcessor.Deserialiser.ImportCustomers(context, customersXml));

            DataProcessor.Deserialiser.ImportSales(context);
        }

        private static void ExportEntities(CarContext context)
        {
            const string exportDir = @"ImportResults\"; 
            File.WriteAllText($"{exportDir}exportCars.xml",DataProcessor.Serialiser.ExportCars(context));
            File.WriteAllText($"{exportDir}ferrari.xml",DataProcessor.Serialiser.ExportFerrari(context));
            File.WriteAllText($"{exportDir}loclSuppliers.xml", DataProcessor.Serialiser.ExportLocalSuppliers(context));
            File.WriteAllText($"{exportDir}carParts.xml", DataProcessor.Serialiser.ExportCarParts(context));
            File.WriteAllText($"{exportDir}customers.xml", DataProcessor.Serialiser.ExportCustomersWithSales(context));
            File.WriteAllText($"{exportDir}sales.xml", DataProcessor.Serialiser.ExportSales(context));
        }

        private static void ResetDatabase(CarContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            
        }
    }
}
