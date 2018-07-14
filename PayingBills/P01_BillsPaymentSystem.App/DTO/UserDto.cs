using System.Collections.Generic;
using P01_BillsPaymentSystem.Data.Models;

namespace P01_BillsPaymentSystem.App.DTO
{
    public class UserDto
    {
        public string Name { get; set; }

        public ICollection<BankAccount> BankAccounts { get; set; }

        public ICollection<CreditCard> CreditCards { get; set; }
    }
}
