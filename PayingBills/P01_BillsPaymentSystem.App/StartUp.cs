using System;
using System.Linq;
using System.Text;
using P01_BillsPaymentSystem.App.DTO;
using P01_BillsPaymentSystem.Data;
using P01_BillsPaymentSystem.Data.Models;
using Serialiser;

namespace P01_BillsPaymentSystem.App
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            //int id = int.Parse(Console.ReadLine());
            using (var db = new BillsPaymentSystemContext())
            {
                //db.Database.EnsureDeleted();
                //db.Database.EnsureCreated();
                //Initialiser.Seed(db);

                int id = int.Parse(Console.ReadLine());
                var user = GetUser(db, id);

                if (user == null)
                {
                    Console.WriteLine($"User with id {id} not found!");
                }
                else
                {
                    WriteDetails(user);
                    //---

                    //PayBills(db, id, bill);

                    var bill = decimal.Parse(Console.ReadLine());

                    PayBills(db, id, bill);
                }
            }
        }

        private static void PayBills(BillsPaymentSystemContext db, int id, decimal bill)
        {
            using (db = new BillsPaymentSystemContext())
            {

                db.Database.BeginTransaction();

                var user = db.Users.Where(p => p.UserId == id)
                    .Select(p => new
                    {
                        Id = p.UserId,
                        BankAccounts = p.PaymentMethods
                            .Where(k => k.BankAccountId != null)
                            .Select(k => k.BankAccount)
                            .OrderBy(k => k.BankAccountId)
                            .ToArray(),
                        CreditCards = p.PaymentMethods
                            .Where(k => k.CreditCardId != null)
                            .Select(k => k.CreditCard)
                            .OrderBy(k => k.CreditCardId)
                            .ToArray()

                    })
                    .SingleOrDefault();


                var totalUserMoney = user.BankAccounts.Sum(p => p.Balance) + user.CreditCards.Sum(p => p.LimitLeft);

                if (!HaveEnoughMoney(totalUserMoney, bill))
                {
                    Console.WriteLine("Insufficient funds!");
                    db.Database.RollbackTransaction();
                    return;
                }

                foreach (var account in user.BankAccounts)
                {
                    account.Withdraw(ref bill);

                    if (bill == 0)
                        break;

                }

                foreach (var card in user.CreditCards)
                {
                    card.Withdraw(ref bill);
                    if (bill == 0)
                    {
                        break;
                    }

                }

                //db.SaveChanges();
                db.Database.CommitTransaction();
                db.SaveChanges();
            }

        }

        private static bool HaveEnoughMoney(decimal sum, decimal bill)
        {
            return sum >= bill;
        }

        private static UserDto GetUser(BillsPaymentSystemContext db, int id)
        {
            var user = db.Users.Where(p => p.UserId == id)
                .Select(p => new UserDto
                {
                    Name = String.Format("{0} {1}", p.FirstName, p.LastName),
                    BankAccounts = p.PaymentMethods.Where(k => k.BankAccountId != null)
                        .Select(k => k.BankAccount)
                    //k.BankAccountId
                        .ToArray(),
                    CreditCards = p.PaymentMethods.Where(k => k.CreditCardId != null)
                        .Select(k => k.CreditCard)
                        .ToArray()
                })
                .SingleOrDefault();

            return user;
        }

        private static void WriteDetails(UserDto user)
        {
            Console.WriteLine($"User:{user.Name}");
            foreach (var account in user.BankAccounts)
            {
                Console.WriteLine($"Bank Accounts:{Environment.NewLine}" +
                                  $"-- ID: {account.BankAccountId}{Environment.NewLine}" +
                                  $"--- Balance: {account.Balance:f2}{Environment.NewLine}" +
                                  $"--- Bank: {account.BankName}{Environment.NewLine}" +
                                  $"--- SWIFT: {account.SWIFTCode}");
            }

            foreach (var card in user.CreditCards)
            {

                Console.WriteLine($"Credit Cards: {Environment.NewLine}" +
                                  $"-- ID: {card.CreditCardId}{Environment.NewLine}" +
                                  $"--- Limit: {card.Limit:f2}{Environment.NewLine}" +
                                  $"--- Money Owed: {card.MoneyOwed:f2}{Environment.NewLine}" +
                                  $"--- Limit Left: {card.LimitLeft:f2}{Environment.NewLine}" +
                                  $"--- Expiration Date:" +
                                  $" {card.ExpirationDate.Year}/" +
                                  $"{card.ExpirationDate.Month:d2}");
            }
        }
    }
}
