using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper.QueryableExtensions;
using BusTicketSystem.Data;
using BusTicketSystem.Models;
using BusTicketSystem.Service.Contracts;

namespace BusTicketSystem.Service
{
    public class CustomerService : ICustomerService
    {
        readonly BusTicketContext context;

        public CustomerService(BusTicketContext context)
        {
            this.context = context;
        }

        public TModel ById<TModel>(int id)
            => By<TModel>(p => p.Id == id).SingleOrDefault();

        public bool Exists(int id)
            => ById<Customer>(id) != null;

        public bool HasEnoughMoney(int id, decimal price)
        {
            var account = context.BankAccounts.Where(p => p.CustomerId == id).FirstOrDefault();

            return account.Balance >= price;
        }

        public void PayTicket(int id, decimal price)
        {
            var account = context.BankAccounts.Where(p => p.CustomerId == id).FirstOrDefault();

            account.Balance -= price;
            context.SaveChanges();
        }

        private IEnumerable<TModel> By<TModel>(Func<Customer, bool> predicate)
            => this.context
                .Customers
                .Where(predicate)
                .AsQueryable()
                .ProjectTo<TModel>();
    }
}
