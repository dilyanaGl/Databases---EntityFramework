using System;
using System.Collections.Generic;
using System.Text;

namespace P01_BillsPaymentSystem.Data.Models
{
   public class CreditCard
    {
        private CreditCard() { }

        public CreditCard(decimal limit, decimal moneyOwed, DateTime expirationDate)
        {
           Limit = limit;
            MoneyOwed = moneyOwed;
            ExpirationDate = expirationDate;
        }

        public int CreditCardId { get; set; }

        public decimal Limit { get; private set; }

        public decimal MoneyOwed { get; private set; }

        public decimal LimitLeft { get; private set; }

        public DateTime ExpirationDate { get; set; }

        public ICollection<PaymentMethod> PaymentMethods { get; set; } = new List<PaymentMethod>();

        public void Withdraw(ref decimal money)
        {
            if (this.LimitLeft >= money)
            {
                this.LimitLeft -= money;
            }
            else
            {
                
                money -= this.LimitLeft;
                this.LimitLeft = 0;

            }
        }

        public void Deposit(ref decimal money)
        {
            this.LimitLeft += money;
        }
    }
}
