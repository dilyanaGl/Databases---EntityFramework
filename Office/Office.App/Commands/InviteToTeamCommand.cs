using System;
using System.Collections.Generic;
using System.Text;
using Office.App.Contracts;
using Office.Models;
using Office.Services;
using Office.Services.Contracts;

namespace Office.App.Commands
{
    public class InviteToTeamCommand : ICommand
    {
        private const string SuccessMessage = "Team {0} invited {1}!";
        private const string NotAllowed = "Not allowed!";
        private const string NotExist = "Team or user does not exist!";
        private const string InviteAlreadySent = "Invite is already sent!";
        private const string NoUser = "You should login first!";
        private const string InvalidLength = "Invalid arguments count!";

        private readonly IUserService userService;
        private readonly IEventService eventService;
        private readonly IInvitationService invitationService;
        private readonly ITeamService teamService;

        public InviteToTeamCommand(IUserService userService, IEventService eventService, IInvitationService invitationService, ITeamService teamService)
        {
            this.userService = userService;
            this.eventService = eventService;
            this.invitationService = invitationService;
            this.teamService = teamService;
        }
        
        public string Execute(string[] data)
        {

            if (!Validation.CheckLength(2, data))
            {
                return InvalidLength;
            }

            if (Session.User == null)
            {
                return NoUser;
            }

            string username = data[1];
            string teamName = data[0];

            if (!userService.Exist(username) || !teamService.Exists(teamName))
            {
                return NotExist;
            }


            if (!teamService.IsCreator(teamName, Session.User.Username))
            {
                return NotAllowed;

            }

            if (invitationService.Exists(username, teamName))
            {
                return InviteAlreadySent;

            }

            invitationService.InviteUser(teamName, username);

            return String.Format(SuccessMessage, teamName, username);

        }
    }
}

