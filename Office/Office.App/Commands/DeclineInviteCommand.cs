using System;
using System.Collections.Generic;
using System.Text;
using Office.App.Contracts;
using Office.Models;
using Office.Services;
using Office.Services.Contracts;

namespace Office.App.Commands
{
    public class DeclineInviteCommand : ICommand
    {
        private const string SuccessMessage = "Invite from {0} declined.";
        private const string TeamNotFound = "Team {0} not found!";
        private const string NoInvite = "Invite from {0} is not found!";
        private const string NoUser = "You should login first!";
        private const string InvalidLength = "Invalid arguments count!";



        private readonly ITeamService teamService;
        private readonly IInvitationService invitationService;

        public DeclineInviteCommand(ITeamService teamService, IInvitationService invitationService)
        {
            this.teamService = teamService;
            this.invitationService = invitationService;

        }

        public string Execute(string[] data)
        {
            if (!Validation.CheckLength(1, data))
            {
                return InvalidLength;
            }

            if (Session.User == null)
            {
                return NoUser;
            }

            string teamName = data[0];

            if (!teamService.Exists(teamName))
            {
                return String.Format(TeamNotFound, teamName);
            }

            if (!invitationService.Exists(teamName, Session.User.Username))
            {
                return String.Format(NoInvite, teamName);

            }

            var invitationId = invitationService.ByTeamAndUserName(teamName, Session.User.Username).Id;

            invitationService.DeclineInvite(invitationId);

            return String.Format(SuccessMessage, teamName, Session.User.Username);

        }
    }
}
