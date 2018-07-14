using System.Collections.Generic;

namespace P01_BillsPaymentSystem.Data.Models
{
    public class BankAccount
    {
        private BankAccount() { }

        public BankAccount(decimal balance, string name, string swiftCode)
        {
            this.Balance = balance;
            this.BankName = name;
            this.SWIFTCode = swiftCode;
        }
 
        public int BankAccountId { get; set; }

        public decimal Balance { get; private set; }

        public string BankName { get; set; }

        public string SWIFTCode { get; set; }

        public ICollection<PaymentMethod> PaymentMethods { get; set; } = new List<PaymentMethod>();

        public void Deposit(ref decimal money)
        {
            this.Balance += money;
        }

        public void Withdraw(ref decimal money)
        {
            if (this.Balance >= money)
            {
                this.Balance -= money;
            }
            else
            {
                money -= this.Balance;
                this.Balance = 0;
            }
        }
    }
}