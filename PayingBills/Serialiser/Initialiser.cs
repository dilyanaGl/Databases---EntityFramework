using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using P01_BillsPaymentSystem.Data;
using P01_BillsPaymentSystem.Data.Models;

namespace Serialiser
{
    public static class Initialiser
    {
        public static void Seed(BillsPaymentSystemContext context)
        {
            InitialiseUsers(context);
            InitialiseBankAccounts(context);
            InitialiseCreditCards(context);
            InitialisePaymentMehotds(context);
        }

        private static void InitialisePaymentMehotds(BillsPaymentSystemContext context)
        {
            var payments = PaymentMethodInitialiser.GetPayments();
            var validPayments = new List<PaymentMethod>();
            for (int i = 0; i < payments.Length; i++)
            {
                if (IsValid(payments[i]))
                {
                    validPayments.Add(payments[i]);
                }
            }

            context.AddRange(validPayments);
            context.SaveChanges();

        }

        private static bool IsValid(object obj)
        {
            var validationContext = new ValidationContext(obj);
            var result = new List<ValidationResult>();
            return Validator.TryValidateObject(obj, validationContext, result);
        }

        private static void InitialiseBankAccounts(BillsPaymentSystemContext context)
        {
            var bankAccounts = BankAccountsInitialiser.GetBankAccounts();
            var validBankAccounts = new List<BankAccount>();
            for (int i = 0; i < bankAccounts.Length; i++)
            {
                if (IsValid(bankAccounts[i]))
                {
                    validBankAccounts.Add(bankAccounts[i]);
                }
            }
            context.AddRange(validBankAccounts);
            context.SaveChanges();

        }

        private static void InitialiseCreditCards(BillsPaymentSystemContext context)
        {
            var creditCards = CreditCardInitialiser.GetCreditCards();
            var validCreditCards = new List<CreditCard>();
            for (int i = 0; i < creditCards.Length; i++)
            {
                if (IsValid(creditCards[i]))
                {
                    validCreditCards.Add(creditCards[i]);
                }
            }
            context.AddRange(validCreditCards);
            context.SaveChanges();
        }

        private static void InitialiseUsers(BillsPaymentSystemContext context)
        {
            var users = UsersInitiliser.GetUsers();
            var validUsers = new List<User>();
            for (int i = 0; i < users.Length; i++)
            {
                if (IsValid(users[i]))
                {
                    validUsers.Add(users[i]);
                }
            }
            context.AddRange(validUsers);
            context.SaveChanges();
        }
    }
}

