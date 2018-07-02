using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using BookShop.Models;

namespace BookShop
{
    using BookShop.Data;
    using BookShop.Initializer;

    public class StartUp
    {
        static void Main()
        {
            using (var db = new BookShopContext())
            {
                DbInitializer.ResetDatabase(db);
                string categories = Console.ReadLine();
                Console.WriteLine(GetAuthorNamesEndingIn(db, categories));

            }
        }

        public static string GetBooksReleasedBefore(BookShopContext context, string date)
        {
            var sb = new StringBuilder();

            DateTime releaseDate = DateTime.ParseExact(date, "dd-MM-yyyy", CultureInfo.InvariantCulture);

            var books = context.Books
                .Where(p => p.ReleaseDate < releaseDate)
                .OrderByDescending(p => p.ReleaseDate)
                .Select(p => new
                {
                    p.Title,
                    p.EditionType,
                    p.Price
                })
                .ToArray();

            foreach (var book in books)
            {
                sb.AppendLine($"{book.Title} - {book.EditionType} - ${book.Price:f2}");
            }

            return sb.ToString().Trim();
        }

        public static string GetAuthorNamesEndingIn(BookShopContext context, string input)
        {
            var sb = new StringBuilder();

            var authors = context.Authors.Where(p => p.FirstName.EndsWith(input))
                .Select(p => new
                {
                    Name = $"{p.FirstName} {p.LastName}"

                })
                .OrderBy(p => p.Name)
                .ToArray();

            foreach (var author in authors)
            {
                sb.AppendLine(author.Name);
            }
            return sb.ToString().Trim();
        }

        public static string GetBookTitlesContaining(BookShopContext context, string input)
        {
            var sb = new StringBuilder();

            var books = context.Books
                .Where(p => p.Title.IndexOf(input, StringComparison.OrdinalIgnoreCase) >= 0)
                .Select(p => p.Title)
                .ToArray();

            foreach (var author in books)
            {
                sb.AppendLine(author);
            }

            return sb.ToString().Trim();
        }

        public static string GetBooksByCategory(BookShopContext context, string input)
        {
            var sb = new StringBuilder();

            var categories = input.Split().ToArray();

            var books = context.Categories
                .Where(p => categories.Any(l => l.Equals(p.Name, StringComparison.OrdinalIgnoreCase)))
                .Select(p => p.CategoryBooks
                    .Select(g => g.Book)
                    .Select(b => b.Title))
                .OrderBy(p => p)
                   .ToArray();

            var titles = new List<string>();


            foreach (var bt in books)
            {
                foreach (var book in bt)
                {
                    titles.Add(book);
                }
            }

            foreach (var title in titles.OrderBy(p => p))
            {
                sb.AppendLine(title);
            }
            return sb.ToString().Trim();
        }

        public static string GetBooksNotRealeasedIn(BookShopContext context, int year)
        {
            var sb = new StringBuilder();

            var books = context.Books
                .Where(p => p.ReleaseDate.Value.Year != year)
                .OrderBy(p => p.BookId)
                .Select(p => p.Title)
                .ToArray();

            foreach (var book in books)
            {
                sb.AppendLine(book);
            }

            return sb.ToString().Trim();
        }

        public static string GetBooksByPrice(BookShopContext context)
        {
            var sb = new StringBuilder();

            var books = context.Books
                .Where(p => p.Price > 40)
                .Select(p => new
                {
                    p.Title,
                    p.Price
                }
                )
                .OrderByDescending(p => p.Price)
                .ToArray();

            foreach (var book in books)
            {
                sb.AppendLine($"{book.Title} - ${book.Price:f2}");
            }

            return sb.ToString().Trim();

        }

        public static string GetGoldenBooks(BookShopContext context)
        {
            var sb = new StringBuilder();

            var books = context.Books
                .Where(p => p.Copies < 5000)
                .OrderBy(p => p.BookId)
                .Select(p => new
                {
                    p.Title,
                    p.EditionType
                })
                .ToArray();

            foreach (var book in books)
            {
                if (book.EditionType.ToString() == "Gold")
                {
                    sb.Append(book.Title + Environment.NewLine);
                }
            }
            return sb.ToString().Trim();

        }

        public static string GetBooksByAuthor(BookShopContext context, string input)
        {
            var sb = new StringBuilder();

            var books = context.Books
                .Where(p => p.Author.LastName.StartsWith(input, StringComparison.OrdinalIgnoreCase))
                .OrderBy(p => p.BookId)
                .Select(p => new
                {
                    Title = p.Title,
                    Author = $"{p.Author.FirstName} {p.Author.LastName}"

                })
                .ToArray();

            foreach (var book in books)
            {
                sb.AppendLine($"{book.Title} ({book.Author})");
            }

            return sb.ToString().Trim();
        }

        public static string GetBooksByAgeRestriction(BookShopContext context, string command)
        {
            var currentString = Char.ToUpper(command[0]) + command.Substring(1).ToLower();

            var sb = new StringBuilder();

            try
            {
                AgeRestriction enumValue = (AgeRestriction)Enum.Parse(typeof(AgeRestriction), currentString);


                var books = context.Books
                    .Where(p => p.AgeRestriction == enumValue)
                    .Select(p => p.Title)
                    .OrderBy(p => p)
                    .ToArray();

                foreach (var book in books)
                {
                    sb.AppendLine(book);
                }
            }
            catch
            {
                sb.AppendLine("Invalid age restriction");
            }

            return sb.ToString().Trim();

        }

        public static int CountBooks(BookShopContext context, int check)
        {
            var count = context.Books
                .Count(p => p.Title.Length > check);

            return count;

        }

        public static string CountCopiesByAuthor(BookShopContext context)
        {
            var  sb = new StringBuilder();
            var authors = context.Authors
                .Select(p => new
                {
                    Name = $"{p.FirstName} {p.LastName}",
                    Count = p.Books.Sum(k => k.Copies)
                })
                .OrderByDescending(p => p.Count)
                .ToArray();

            foreach (var author in authors)
            {
                sb.AppendLine($"{author.Name} - {author.Count}");
            }

            return sb.ToString().Trim();
        }


        public static string GetTotalProfitByCategory(BookShopContext context)
        {
            var sb = new StringBuilder();

            var categories = context.Categories
                .Select(p => new
                    {
                        Name = p.Name,
                        Profit = p.CategoryBooks.Sum(k => k.Book.Copies * k.Book.Price)
                    }
                )
                .OrderByDescending(p => p.Profit)
                .ThenBy(p => p.Name)
                .ToArray();

            foreach (var category in categories)
            {
                sb.AppendLine($"{category.Name} ${category.Profit:f2}");
            }

            return sb.ToString().Trim();
        }

        public static string GetMostRecentBooks(BookShopContext context)
        {
            var sb = new StringBuilder();

            var categories = context.Categories
                .OrderBy(p => p.CategoryBooks.Select(k => k.Book).Count())
                .Select(p => new
                {
                    p.Name,
                    Books = p.CategoryBooks.Select(k => k.Book).OrderByDescending(k => k.ReleaseDate)
                        .Select(k => new
                        {
                            k.Title, 
                            k.ReleaseDate
                        })
                        .Take(3)
                        .ToArray()
                })
                .ToArray();

            foreach (var category in categories)
            {
                sb.AppendLine($"--{category.Name}");
                foreach (var book in category.Books)
                {
                    sb.AppendLine($"{book.Title} ({book.ReleaseDate.Value.Year})");
                }
            }



            return sb.ToString().Trim();
        }

        public static void IncreasePrices(BookShopContext context)
        {
            var books = context.Books.Where(p => p.ReleaseDate.Value.Year < 2010).ToArray();

            for (int i = 0; i < books.Length; i++)
            {
                books[i].Price += 5;
            }
        }

    }
}
