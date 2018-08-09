using System;
using System.IO;
using JsonProcessing.Data;

namespace JsonProcessing.App
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var db = new ProductContext();
            ResetDatabase(db);

            Console.WriteLine("Database Reset.");

            ImportEntities(db);
            ExportEntities(db);
        }

        private static void ImportEntities(ProductContext db, string baseDir = @"Data\")
        {

            var usersString = File.ReadAllText($"{baseDir}users.json");
            Console.WriteLine(DataProcessor.Deserializer.ImportUsers(db, usersString));

            var categoryString = File.ReadAllText($"{baseDir}categories.json");
            Console.WriteLine(DataProcessor.Deserializer.ImportCategories(db, categoryString));


            var productString = File.ReadAllText($"{baseDir}products.json");
            Console.WriteLine(DataProcessor.Deserializer.ImportProducts(db, productString));

            DataProcessor.Deserializer.ImportProductsCateogories(db);
        }

        private static void ExportEntities(ProductContext db)
        {
            const string baseDir = @"ExportResults\";

            var productsInRangePath = $"{baseDir}productsInRange.json";
            File.WriteAllText(
                productsInRangePath,
                DataProcessor.Serializer.ExportProductsInRange(db));

            var exportSoldProductPath = $"{baseDir}soldProducts.json";
            File.WriteAllText(
                exportSoldProductPath,
                DataProcessor.Serializer.ExportUsersWithSoldProducts(db));


            var productsByCountPath = $"{baseDir}productsCategories.json";
            File.WriteAllText(productsByCountPath, DataProcessor.Serializer.ExportProductByCount(db));

            var categoryProductsPath = $"{baseDir}categoriesProducts.json";
            File.WriteAllText(categoryProductsPath, DataProcessor.Serializer.ExportCategoriesProducts(db));

            var userProductsPath = $"{baseDir}userProducts.json";
            File.WriteAllText(userProductsPath, DataProcessor.Serializer.ExportUsersProducts(db));

        }

        private static void ResetDatabase(ProductContext db)
        {
            db.Database.EnsureDeleted();

            db.Database.EnsureCreated();
        }
    }
}
