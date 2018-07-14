using System;
using System.Collections.Generic;
using System.Text;
using P01_BillsPaymentSystem.Data.Models;

namespace Serialiser
{
   public static class BankAccountsInitialiser
   {
       private static string[] bankNames = new string[]
       {
           "DSK",
           "HSBC",
           "Loyds",
           "Hall",
           "UniBank",
           "Abc"
       };

       private static string[] swiftCodes = new string[]
       {
           "123Hh",
           "263thjshdj",
           "23ejlwk3",
           "djkdjhk3r34",
           "kjhkjsd232"
       };

       public static BankAccount[] GetBankAccounts()
       {
            var bankAccounts = new BankAccount[10];
           var rnd = new Random();

           for (int i = 0; i < 10; i++)
           {
               string bankName = bankNames[rnd.Next(0, bankNames.Length - 1)];
               string swiftCode = swiftCodes[rnd.Next(0, swiftCodes.Length - 1)];
               decimal balance = rnd.Next(0, 19992) / (decimal) rnd.Next(1, 1212);


               var account = new BankAccount(balance, bankName, swiftCode);
               //{
               //    //BankAccountId = i + 1,
               //    //Balance = rnd.Next(0, 19992) / (decimal)rnd.Next(1, 1212),
               //    //BankName = bankName, 
               //    //SWIFTCode = swiftCode

               //};

               bankAccounts[i] = account;
           }
          

           return bankAccounts;
       }


   }
}
