using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Data;
using DataProcessor.Dto.Export;

namespace DataProcessor
{
    public static class Serializer
    {
        public static string ExportProductsInRange(ProductContext context)
        {

            var products = context.Products
                .Where(p => p.Price >= 1000 && p.Price <= 2000)
                .Select(p => new ProductDto
                {
                    Name = p.Name,
                    Price = p.Price,
                    Buyer = $"{p.Buyer.FirstName} {p.Buyer.LastName}"

                })
                .OrderBy(p => p.Price)
                .ToArray();

            var sb = new StringBuilder();

            var serializer = new XmlSerializer(typeof(ProductDto[]), new XmlRootAttribute("products"));

            serializer.Serialize(new StringWriter(sb),
                products,
                new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty }));

            var result = sb.ToString();
            return result;




        }

        public static string ExportSoldProduct(ProductContext context)
        {

            var sb = new StringBuilder();

            var users = context.Users
                .Where(p => p.ProductsSold.Count() >= 0)
                .Select(p => new SellerDto
                {
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    SoldProducts = p.ProductsSold.Select(k => new SoldProductDto
                    {
                        Name = k.Name,
                        Price = k.Price
                    })
                        .ToArray()
                })
                .OrderBy(p => p.LastName)
                .ThenBy(p => p.FirstName)
                .ToArray();


            var serializer = new XmlSerializer(typeof(SellerDto[]), new XmlRootAttribute("users"));

            serializer.Serialize(new StringWriter(sb),
                users,
                new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty }));

            return sb.ToString();



        }

        public static string ExportProductByCount(ProductContext context)
        {
            var sb = new StringBuilder();

            var categories = context.Categories.Select(p => new CategoryDto
            {

                Name = p.Name,
                ProductsCount = p.Products.Count(),
                AveragePrice = p.Products.Any() ? p.Products.Select(k => k.Product.Price).Average() : 0,
                TotalRevenue = p.Products.Any() ? p.Products.Select(k => k.Product.Price).Sum() : 0
            })
                .OrderByDescending(p => p.ProductsCount)
                .ToArray();



            var serializer = new XmlSerializer(typeof(CategoryDto[]), new XmlRootAttribute("categories"));

            serializer.Serialize(new StringWriter(sb),
                categories,
                new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty }));



            return sb.ToString();



        }

        public static string ExportUserProducts(ProductContext context)
        {

            var sb = new StringBuilder();

            var userProducts = context.Users.Where(p => p.ProductsSold.Any())
                .Select(p => new UserDto
                {
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    Age = p.Age,
                    SoldProducts = new SellerProductDto
                    {
                        Count = p.ProductsSold.Count(),
                        Products = p.ProductsSold.Select(k => new SellerSoldProductDto
                        {
                            Name = k.Name,
                            Price = k.Price
                        })
                            .ToArray()
                    }
                })
                .OrderByDescending(p => p.SoldProducts.Count)
                .ThenBy(p => p.LastName)
                .ToArray();



            var totalUsers = new UserProductDto
            {
                Count = userProducts.Length,
                Users = userProducts
            };


            var serializer = new XmlSerializer(typeof(UserProductDto), new XmlRootAttribute("users"));

            serializer.Serialize(new StringWriter(sb),
                        totalUsers,
                        new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty
        }));


            return sb.ToString();

        }


    }
}
