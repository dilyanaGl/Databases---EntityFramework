using System;
using System.Collections.Generic;
using System.Text;
using P01_BillsPaymentSystem.Data.Models;
using Type = P01_BillsPaymentSystem.Data.Models.Type;

namespace Serialiser
{
    public static class PaymentMethodInitialiser
    {
        public static PaymentMethod[] GetPayments()
        {
            var rnd = new Random();
            var payments = new PaymentMethod[10];
            for (int i = 0; i < 10; i++)
            {
                int? bankAccountId = rnd.Next(1, 10);
                int? creditCardId = rnd.Next(1, 10);
                var typeIndex = rnd.Next(1, 2);
               int userId = rnd.Next(1, 10);

                if ((bankAccountId % 3) + 7 == 0)
                {
                    creditCardId = null;
                }
                else
                {
                    bankAccountId = null;
                }

                var type = bankAccountId == null ? Type.BankAccount : Type.CreditCard;

                var paymentMethod = new PaymentMethod()
                {
                    BankAccountId = bankAccountId, 
                    CreditCardId = creditCardId, 
                    Type = type,
                    UserId = userId

                };

                payments[i] = paymentMethod;
            }
            
            return payments;
        }
    }
}
