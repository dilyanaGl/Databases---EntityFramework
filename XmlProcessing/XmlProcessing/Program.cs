using System;
using System.IO;
using Data;

namespace XmlProcessing
{
    class Program
    {
        static void Main(string[] args)
        {
            var db = new ProductContext();
            ResetDatabase(db);

            Console.WriteLine("Database Reset.");

            ImportEntities(db);
            ExportEntities(db);
        }

        private static void ImportEntities(ProductContext db, string baseDir = @"Data\")
        {

            var usersString = File.ReadAllText($"{baseDir}users.xml");
            Console.WriteLine(DataProcessor.Deserializer.ImportUsers(db, usersString));

            var categoryString = File.ReadAllText($"{baseDir}categories.xml");
            Console.WriteLine(DataProcessor.Deserializer.ImportCategories(db, categoryString));


            var productString = File.ReadAllText($"{baseDir}products.xml");
            Console.WriteLine(DataProcessor.Deserializer.ImportProducts(db, productString));


            DataProcessor.Deserializer.ImportProductsCateogories(db);
        }

        private static void ExportEntities(ProductContext db)
        {
            const string baseDir = @"ExportResults\";

            var productsInRangePath = $"{baseDir}productsInRange.xml";
            File.WriteAllText(
                productsInRangePath,
                DataProcessor.Serializer.ExportProductsInRange(db));

            var exportSoldProductPath = $"{baseDir}soldProducts.xml";
            File.WriteAllText(
                exportSoldProductPath,
                DataProcessor.Serializer.ExportSoldProduct(db));


            var productsByCountPath = $"{baseDir}productsCategories.xml";
            File.WriteAllText(productsByCountPath, DataProcessor.Serializer.ExportProductByCount(db));

            var userProductsPath = $"{baseDir}userProducts.xml";
            File.WriteAllText(userProductsPath, DataProcessor.Serializer.ExportUserProducts(db));

        }

        private static void ResetDatabase(ProductContext db)
        {
            db.Database.EnsureDeleted();

            db.Database.EnsureCreated();
        }

    }
}
