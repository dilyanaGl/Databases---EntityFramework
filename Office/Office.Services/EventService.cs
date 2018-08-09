using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper.QueryableExtensions;
using Office.Data;
using Office.Models;
using Office.Services.Contracts;

namespace Office.Services
{
    public class EventService : IEventService
    {
        private readonly OfficeContext context;

        public EventService(OfficeContext context)
        {
            this.context = context;

        }

        public Event ByName(string name)
            => context.Events.Where(p => p.Name == name)
                .OrderByDescending(p => p.StartDate)
                .FirstOrDefault();

        public bool Exists(string name)
            => ByName(name) != null;

        public void CreateEvent(string name, string description, DateTime startDate, DateTime endDate,
            string creatorName)
        {

            var creator = context.Users.SingleOrDefault(p => p.Username == creatorName);

            var @event = new Event
            {
                Name = name,
                Description = description, 
                StartDate = startDate,
                EndDate = endDate,
                Creator = creator
            };


            context.Add(@event);
            context.SaveChanges();
        }

        public string ShowEvent(string eventName)
        {
            return context.Events.Where(p => p.Name == eventName)
                .OrderByDescending(p => p.StartDate)
                .First()
                .ToString();
        }
      
        private IEnumerable<TModel> By<TModel>(Func<Event, bool> predicate)
            => this.context
                .Events
                .Where(predicate)
                .AsQueryable()
     .ProjectTo<TModel>();

    }

}
