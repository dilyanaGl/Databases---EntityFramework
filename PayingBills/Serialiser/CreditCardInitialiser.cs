using System;
using System.Collections.Generic;
using System.Text;
using P01_BillsPaymentSystem.Data.Models;

namespace Serialiser
{
    public static class CreditCardInitialiser
    {
        public static CreditCard[] GetCreditCards()
        {
            var creditCards = new CreditCard[10];


            for (int i = 0; i < creditCards.Length; i++)
            {
                var rnd = new Random();
                var limit = rnd.Next(0, 1212121) / (decimal) rnd.Next(1, 2323);
                var moneyOwed = rnd.Next(0, 1212121) / (decimal)rnd.Next(1, 2323);
                var expirationDate = DateTime.Now.AddDays(rnd.Next(0, 1210));

                var creditCard = new CreditCard(limit, moneyOwed, expirationDate);
           

                creditCards[i] = creditCard;
            }

            return creditCards;

        }
    }
}
