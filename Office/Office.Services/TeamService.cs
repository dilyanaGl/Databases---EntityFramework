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
    public class TeamService : ITeamService
    {
        private readonly OfficeContext context;

        public TeamService(OfficeContext context)
        {
            this.context = context;
        }


        public Team ByName(string name)
            => context.Teams.SingleOrDefault(p => p.Name == name);

        public bool Exists(string name)
            => ByName(name) != null;


        public bool IsCreator(string teamName, string username)
        {

            var team = context.Teams.SingleOrDefault(p => p.Name == teamName);
            return username == team.Creator.Username;

        }

        public bool IsTeamMember(string teamName, string username)
        {

            return context.UserTeams.Any(p => p.Team.Name == teamName && p.User.Username == username);

        }


        public void Delete(string name)
        {
            var team = context.Teams.SingleOrDefault(p => p.Name == name);
            context.Teams.Remove(team);
            context.SaveChanges();

        }

        public void CreateTeam(string name, string acronym, string description, string username)
        {
            var creator = context.Users.SingleOrDefault(p => p.Username == username);
            var team = new Team
            {
                Name = name,
                Acronym = acronym,
                Description = description,
                Creator = creator
            };

            context.Add(team);
            context.SaveChanges();

        }

        public string ShowTeam(string teamName)
        {
            var team = context.Teams.SingleOrDefault(p => p.Name == teamName);
            return team.ToString();
        }

    }
}
