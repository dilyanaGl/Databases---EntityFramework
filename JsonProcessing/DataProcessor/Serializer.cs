using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataProcessor.Dto.Export;
using JsonProcessing.Data;
using Newtonsoft.Json;

namespace DataProcessor
{
    public static class Serializer
    {
        public static string ExportProductsInRange(ProductContext context)
        {

            var products = context.Products.Where(p => p.Price >= 500 && p.Price <= 1000)
                .Select(p => new ProductDto
                {
                    Name = p.Name,
                    Price = p.Price,
                    Seller = $"{p.Seller.FirstName} {p.Seller.LastName}",
                })
                .OrderBy(p => p.Price)
                .ToArray();


            var jsonString = JsonConvert.SerializeObject(products, Formatting.Indented);

            return jsonString;

        }

        public static string ExportUsersWithSoldProducts(ProductContext context)
        {


            var userSoldProducts = context.Users.Where(p => p.ProductsSold.Count > 0)
                .Select(p => new UserDto
                {
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    SoldProducts = p.ProductsSold.Where(k => k.Buyer != null)
                        .Select(k => new SoldProductDto
                        {

                            Name = k.Name,
                            Price = k.Price,
                            BuyerFirstName = k.Buyer.FirstName,
                            BuyerLastName = k.Buyer.LastName
                        }).ToArray()
                })
                .OrderBy(p => p.LastName)
                .ThenBy(p => p.FirstName)
                .ToArray();




            var jsonString = JsonConvert.SerializeObject(userSoldProducts, Formatting.Indented);

            return jsonString;


        }

        public static string ExportCategoriesProducts(ProductContext context)
        {

            var categories = context.Categories.Select(p => new CategoryDto
            {

                Category = p.Name,
                ProductsCount = p.Products.Count(),
                AveragePrice = p.Products.Any() ? p.Products.Average(k => k.Product.Price) : 0,
                TotalRevenue = p.Products.Any() ? p.Products.Sum(k => k.Product.Price) : 0


            })
                .OrderBy(p => p.ProductsCount)
                .ToArray();





            var jsonString = JsonConvert.SerializeObject(categories, Formatting.Indented);

            return jsonString;


        }

        public static string ExportProductByCount(ProductContext context)
        {
            var sb = new StringBuilder();

            var categories = context.Categories.Select(p => new CategoryProductDto
            {

                Name = p.Name,
                ProductsCount = p.Products.Any() ? p.Products.Count() : 0,
                AveragePrice = p.Products.Any() ? p.Products.Select(l => l.Product.Price).Average() : 0,
                TotalRevenue = p.Products.Any() ? p.Products.Select(l => l.Product.Price).Sum() : 0
            })
                .OrderByDescending(p => p.ProductsCount)
                .ToArray();




            var jsonString = JsonConvert.SerializeObject(categories, Formatting.Indented);

            return jsonString;
        }

        public static string ExportUsersProducts(ProductContext context)
        {
            var users = context.Users.Where(p => p.ProductsSold.Any())
                .Select(p => new UsersProductDto
                {
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    Age = p.Age,
                    SoldProducts = new ProductSoldDto
                    {
                        Count = p.ProductsSold.Count(),
                        Products = p.ProductsSold.Select(k => new ProductExportDto
                        {
                            Name = k.Name,
                            Price = k.Price
                        })
                            .ToArray()
                    }
                })
                .ToArray();

        var totalUserDto = new TotalUserDto
        {
            Count = users.Length,
            Users = users
        };



        var jsonString = JsonConvert.SerializeObject(users, Formatting.Indented);

        return jsonString;

}

}


}

