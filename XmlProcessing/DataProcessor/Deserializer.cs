using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Data;
using DataModels;
using DataProcessor.Dto.Import;

namespace DataProcessor
{
    public static class Deserializer
    {
        private const string FailureMessage = "Invalid details!";
        private const string SuccessMessage = "Successfully imported {0}";
        private const string SuccessUserMessage = "Successfully imported {0} {1}";

        public static string ImportUsers(ProductContext context, string xmlString)
        {

            var sb = new StringBuilder();

            var serializer = new XmlSerializer(typeof(UserDto[]), new XmlRootAttribute("users"));
            var deserializedUsers =
                (UserDto[]) serializer.Deserialize(new MemoryStream(Encoding.UTF8.GetBytes(xmlString)));

            var validUsers = new List<User>();


            foreach (var obj in deserializedUsers)
            {

                if (!IsValid(obj))
                {

                    sb.AppendLine(FailureMessage);
                    continue;

                }


                var user = new User
                {
                    FirstName = obj.FirstName,
                    LastName = obj.LastName,
                    Age = obj.Age
                };


                validUsers.Add(user);
                sb.AppendLine(String.Format(SuccessUserMessage, obj.FirstName, obj.LastName));

            }

            context.AddRange(validUsers);
            context.SaveChanges();



            return sb.ToString();

        }

        public static string ImportCategories(ProductContext context, string xmlString)
        {

            var sb = new StringBuilder();

            var serializer = new XmlSerializer(typeof(CategoryDto[]), new XmlRootAttribute("categories"));
            var deserializedCategories = (CategoryDto[])serializer.Deserialize(new MemoryStream(Encoding.UTF8.GetBytes(xmlString)));
            
            var validCategories = new List<Category>();

            foreach (var obj in deserializedCategories)
            {

                if (!IsValid(obj))
                {

                    sb.AppendLine(FailureMessage);
                    continue;

                }


                var category = new Category
                {

                    Name = obj.Name

                };

                validCategories.Add(category);
                sb.AppendLine(String.Format(SuccessMessage, obj.Name));

            }

            context.Categories.AddRange(validCategories);
            context.SaveChanges();

            return sb.ToString().Trim();
        }

        public static string ImportProducts(ProductContext context, string xmlString)
        {


            var serializer = new XmlSerializer(typeof(ProductDto[]), new XmlRootAttribute("products"));
            var deserializedProducts = (ProductDto[])serializer.Deserialize(new MemoryStream(Encoding.UTF8.GetBytes(xmlString)));


            var sb = new StringBuilder();

            var users = context.Users.ToArray();
            var rnd = new Random();

            var validProducts = new List<Product>();


            foreach (var obj in deserializedProducts)
            {

                if (!IsValid(obj))
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                var sellerIndex = rnd.Next(0, users.Length - 1);
                int? buyerIndex;
                while (true)
                {

                    buyerIndex = rnd.Next(0, users.Length - 1);

                    if (sellerIndex != buyerIndex)
                    {
                        var product = new Product()
                        {
                            Name = obj.Name,
                            Price = obj.Price,
                            Seller = users[sellerIndex],
                            Buyer = users[buyerIndex.Value],
                           };


                        if ((buyerIndex * sellerIndex) % 5 == 2)
                        {

                            product.Buyer = null;

                        }

                        validProducts.Add(product);
                        sb.AppendLine(String.Format(SuccessMessage, obj.Name));

                        break;

                    }

                }

            }

            context.AddRange(validProducts);
            context.SaveChanges();

            return sb.ToString().Trim();


        }

        public static void ImportProductsCateogories(ProductContext context)
        {
            var products = context.Products.ToArray();
            var categories = context.Categories.ToArray();

            var validCategoryProducts = new List<CategoryProduct>();
            var rnd = new Random();

            foreach (var product in products)
            {
                int count = 0;
                int[] indexes = new int[5];
                while (count < 4)
                {

                    int index = rnd.Next(0, categories.Length - 1);
                    if (!indexes.Contains(index))
                    {
                        indexes[count] = index;
                        var categoryProduct = new CategoryProduct
                        {
                            Product = product,
                            Category = categories[index]
                        };

                        validCategoryProducts.Add(categoryProduct);

                        count++;


                    }


                }

            }

            context.AddRange(validCategoryProducts);

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
