using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusTicketSystem.Data;
using BusTicketSystem.Models;

namespace DataGenerator
{
    public static class BankAccountGenerator
    {
        public static void GenerateBankAccounts(BusTicketContext context)
        {
            string[] accountNumbers = new string[]
            {
                "123343473",
                "343255",
                "34354623423",
                "34354234",
                "3434232434",
                "342377684534"
            };

            var customerIds = context.Customers.Select(p => p.Id).ToArray();

            var validBankAccounts = new List<BankAccount>();

            var rnd = new Random();

            foreach (var id in customerIds)
            {

                var index = rnd.Next(0, accountNumbers.Length - 1);
                decimal balance = rnd.Next(100, 100000) / (decimal) rnd.Next(1, 20);

                var bankAccount = new BankAccount
                {
                    AccountNumber = accountNumbers[index],
                    CustomerId = id,
                    Balance = balance
                };

                validBankAccounts.Add(bankAccount);
            }

            context.AddRange(validBankAccounts);
            context.SaveChanges();
        }
    }
}
