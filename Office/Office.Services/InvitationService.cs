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
    public class InvitationService : IInvitationService
    {
        private readonly OfficeContext context;

        public InvitationService(OfficeContext context)
        {
            this.context = context;

        }

        public Invitation ByTeamAndUserName(string teamName, string username)
            => By(p => p.InvitedUser.Username == username && p.Team.Name == teamName && p.IsActive == true).SingleOrDefault();
        
        public bool Exists(string teamName, string username)
            => ByTeamAndUserName(teamName, username) != null;

        
        public void InviteUser(string teamName, string username)
        {
            var team = context.Teams.SingleOrDefault(p => p.Name == teamName);
            var user = context.Users.SingleOrDefault(p => p.Username == username);

            var invitation = new Invitation
            {
                Team = team,
                InvitedUser = user,
                IsActive = true
            };

            context.Add(invitation);
            context.SaveChanges();
        }

        public void AcceptInvite(string teamname, string username)
        {
            var team = context.Teams.SingleOrDefault(p => p.Name == teamname);
            var user = context.Users.SingleOrDefault(p => p.Username == username);

            var userTeam = new UserTeam
            {

                Team = team,
                User = user

            };

            context.UserTeams.Add(userTeam);
            context.SaveChanges();

        }

        public void DeclineInvite(int inviteId)
        {
            var invitation = context.Invitations.FirstOrDefault(p => p.Id == inviteId);
            context.Invitations.Remove(invitation);
            context.SaveChanges();

        }

        private IEnumerable<Invitation> By(Func<Invitation, bool> predicate)
            => this.context
                .Invitations
                .Where(predicate);


    }
}

