using System;
using System.Collections.Generic;
using System.Text;
using Office.App.Contracts;
using Office.Services.Contracts;

namespace Office.App.Commands
{
    public class ShowTeamCommand : ICommand
    {
        private const string TeamNotFound = "Team {0} not found!";
        private const string InvalidLength = "Invalid arguments count!";

        private readonly ITeamService teamService;

        public ShowTeamCommand(ITeamService teamService)
        {
            this.teamService = teamService;
        }

        public string Execute(string[] data)
        {
            if (!Validation.CheckLength(1, data))
            {
                return InvalidLength;
            }

            string teamName = data[0];
            if (!teamService.Exists(teamName))
            {
                return String.Format(TeamNotFound, teamName);
            }

            return teamService.ShowTeam(teamName);
        }
    }
}
