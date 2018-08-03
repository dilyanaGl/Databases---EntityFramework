using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper.QueryableExtensions;
using PhotoShare.Data;
using PhotoShare.Models;
using PhotoShare.Services.Contracts;

namespace PhotoShare.Services
{
    public class TownService : ITownService
    {
        private readonly PhotoShareContext context;

        public TownService(PhotoShareContext context)
        {
            this.context = context;
        }

        public TModel ById<TModel>(int id)
            => By<TModel>(p => p.Id == id).SingleOrDefault();

        public TModel ByName<TModel>(string name)
            => By<TModel>(p => p.Name == name).SingleOrDefault();

        public bool Exists(int id)
            => ById<Town>(id) != null;

        public bool Exists(string name)
            => ByName<Town>(name) != null;

        public Town Add(string townName, string countryName)
        {
            var town = new Town()
            {
                Name = townName, 
                Country = countryName
            };

            context.Add(town);
            context.SaveChanges();

            return town;
        }

        private IEnumerable<TModel> By<TModel>(Func<Town, bool> predicate)
            => context.Towns.Where(predicate).AsQueryable().ProjectTo<TModel>();
    }
}
