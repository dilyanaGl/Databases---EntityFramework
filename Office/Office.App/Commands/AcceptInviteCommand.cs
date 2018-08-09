using System;
using System.Collections.Generic;
using System.Text;
using Office.App.Contracts;
using Office.Services;
using Office.Services.Contracts;

namespace Office.App.Commands
{
    public class AcceptInviteCommand : ICommand
    {
        private const string SuccessMessage = "User {0} joined team {1}!";
        private const string TeamNotFound = "Team {0} not found!";
        private const string NoInvite = "Invite from {0} is not found!";
        private const string NoUser = "You should login first!";
        private const string InvalidLength = "Invalid arguments count!";


        private readonly IInvitationService invitationService;
        private readonly IEventService eventService;
        private readonly ITeamService teamService;

        public AcceptInviteCommand(IInvitationService invitationService, IEventService eventService,
            ITeamService teamService)
        {
            this.invitationService = invitationService;
            this.eventService = eventService;
            this.teamService = teamService;
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

            invitationService.AcceptInvite(teamName, Session.User.Username);


            return String.Format(SuccessMessage, Session.User.Username, teamName);
        }

    }
}

