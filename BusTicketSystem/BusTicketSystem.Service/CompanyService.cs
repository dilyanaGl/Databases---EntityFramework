using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper.QueryableExtensions;
using BusTicketSystem.Data;
using BusTicketSystem.Models;
using BusTicketSystem.Service.Contracts;

namespace BusTicketSystem.Service
{
    public class CompanyService : ICompanyService
    {
        private readonly BusTicketContext context;

        public CompanyService(BusTicketContext context)
        {

            this.context = context;
        }

        public TModel ById<TModel>(int id)
           => By<TModel>(p => p.Id == id).SingleOrDefault();

        public bool Exists(int id)
            => ById<BusCompany>(id) != null;


        public TModel ByName<TModel>(string name)
            => By<TModel>(p => p.Name == name).SingleOrDefault();

        public bool Exists(string name)
            => ByName<BusCompany>(name) != null;

        private IEnumerable<TModel> By<TModel>(Func<BusCompany, bool> predicate)
            => this.context
                .BusCompanies
                .Where(predicate)
                .AsQueryable()
                .ProjectTo<TModel>();
    }
}
