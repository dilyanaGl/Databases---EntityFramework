using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using DataProcessor.Dto.Import;
using JsonProcessing.Data;
using JsonProcessing.Models;
using Newtonsoft.Json;

namespace DataProcessor
{
    public static class Deserializer
    {
        private const string FailureMessage = "Invalid data!";
        private const string SuccessMessage = "Successfully imported {0}";
        private const string SuccessUserMessage = "Successfully imported {0} {1}";

        public static string ImportUsers(ProductContext context, string jsonString)
        {

            var sb = new StringBuilder();

            var deserialisedUsers = JsonConvert.DeserializeObject<UserDto[]>(jsonString);

            var validUsers = new List<User>();


            foreach (var obj in deserialisedUsers)
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
                    Age = obj.Age.Value
                };


                validUsers.Add(user);
                sb.AppendLine(String.Format(SuccessUserMessage, obj.FirstName, obj.LastName));

            }

            context.AddRange(validUsers);
            context.SaveChanges();



            return sb.ToString();

        }

        public static string ImportCategories(ProductContext context, string jsonString)
        {

            var sb = new StringBuilder();


            var deserialisedCategories = JsonConvert.DeserializeObject<CategoryDto[]>(jsonString);
            var validCategories = new List<Category>();

            foreach (var obj in deserialisedCategories)
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

            context.AddRange(validCategories);
            context.SaveChanges();
            return sb.ToString().Trim();


        }

        public static string ImportProducts(ProductContext context, string jsonString)
        {
            var sb = new StringBuilder();

            var users = context.Users.ToArray();

            var rnd = new Random();

            var deserialisedProducts = JsonConvert.DeserializeObject<ProductDto[]>(jsonString);

            var validProducts = new List<Product>();

            foreach (var obj in deserialisedProducts)
            {
                if (!IsValid(obj))
                {

                    sb.AppendLine(FailureMessage);
                    continue;

                }

                int sellerIndex = rnd.Next(0, users.Length - 1);
                int? buyerIndex;

                while (true)
                {

                    buyerIndex = rnd.Next(0, users.Length - 1);
                    if (buyerIndex != sellerIndex)
                    {

                        var product = new Product
                        {
                            Name = obj.Name,
                            Price = obj.Price,
                            Buyer = users[buyerIndex.Value],
                            Seller = users[sellerIndex]

                        };

                        if (buyerIndex % (sellerIndex + 1) == 2)
                        {
                            product.Buyer = null;

                        }

                        validProducts.Add(product);
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
