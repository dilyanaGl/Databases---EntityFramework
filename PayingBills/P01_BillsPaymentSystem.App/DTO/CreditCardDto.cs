using System;

namespace P01_BillsPaymentSystem.App.DTO
{
    public class CreditCardDto
    {
        public int? Id { get; set; }

        public decimal Limit { get; set; }

        public decimal MoneyOwed { get; set; }

        public decimal LimitLeft { get; set; }

        public DateTime ExpirationDate { get; set; }
          //n       
       
    }
}