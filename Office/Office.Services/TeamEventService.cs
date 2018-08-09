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
    public class TeamEventService : ITeamEventService
    {
        private readonly OfficeContext context;

        public TeamEventService(OfficeContext context)
        {
            this.context = context;

        }

        public TeamEvent ByEventAndTeamName(string eventName, string teamName)
       =>  context.TeamEvents.FirstOrDefault(p => p.Event.Name == eventName && p.Team.Name == teamName);

        public bool Exist(string eventName, string teamName)
            => ByEventAndTeamName(eventName, teamName) != null;

        public void AddTeamTo(string eventName, string teamName)
        {
            var team = context.Teams.SingleOrDefault(p => p.Name == teamName);
            var @event = context.Events.Where(p => p.Name == eventName).OrderByDescending(p => p.StartDate).First();
            var teamEvent = new TeamEvent
            {
                Team = team,
                Event = @event
            };

            context.TeamEvents.Add(teamEvent);
            context.SaveChanges();
        }
        
        private IEnumerable<TModel> By<TModel>(Func<TeamEvent, bool> predicate)
            => this.context
                .TeamEvents
                .Where(predicate)
                .AsQueryable()
                .ProjectTo<TModel>();

    }
}
