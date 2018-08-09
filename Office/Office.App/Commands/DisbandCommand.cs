using System;
using System.Collections.Generic;
using System.Text;
using Office.App.Contracts;
using Office.Services.Contracts;

namespace Office.App.Commands
{
    public class DisbandCommand : ICommand
    {
        private const string SuccessMessage = "{0} has disbanded!";
        private const string TeamNotFound = "Team {0} has disbanded!";
        private const string NotAllowed = "Not Allowed";
        private const string NoUser = "You should login first!";
        private const string InvalidLength = "Invalid arguments count!";

        private readonly ITeamService teamService;
        public DisbandCommand(ITeamService teamService)
        {
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
                return TeamNotFound;
            }

            if (!teamService.IsCreator(teamName, Session.User.Username))
            {
                return NotAllowed;
            }

            teamService.Delete(teamName);

            return String.Format(SuccessMessage, teamName);

        }
    }
}
