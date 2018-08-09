using System;
using System.Collections.Generic;
using System.Text;
using Office.App.Contracts;
using Office.Services;
using Office.Services.Contracts;

namespace Office.App.Commands
{
    public class AddTeamToCommand : ICommand
    {
        private const string SuccessMessage = "Team {0} added for {1}!";
        private const string TeamNotFound = "Team {0} not found!";
        private const string NotAllowed = "Not allowed!";
        private const string NoUser = "You should login first!";
        private const string EventNotFound = "Event [eventName] not found!";
        private const string CannotAddTeamTwice = "Cannot add same team twice!";
        private const string InvalidLength = "Invalid arguments count!";

        private readonly IEventService eventService;
        private readonly ITeamService teamService;
        private readonly ITeamEventService teamEventService;
       

        public AddTeamToCommand(IEventService eventService, ITeamService teamService, ITeamEventService teamEventService)

        {
            this.eventService = eventService;
            this.teamService = teamService;
            this.teamEventService = teamEventService;
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

            string eventName = data[0];
            string teamName = data[1];

            if (!teamService.Exists(teamName))
            {
                return TeamNotFound;
            }

            if (!eventService.Exists(eventName))
            {
                return EventNotFound;
            }

            if (!teamService.IsCreator(teamName, Session.User.Username))
            {
                return NotAllowed;
            }

            if (teamEventService.Exist(eventName, teamName))
            {
                return CannotAddTeamTwice;
            }

            teamEventService.AddTeamTo(eventName, teamName);


            return String.Format(SuccessMessage, teamName, eventName);

        }
    }
}
