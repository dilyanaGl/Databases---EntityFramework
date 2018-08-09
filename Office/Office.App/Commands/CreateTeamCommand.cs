using System;
using System.Collections.Generic;
using System.Text;
using Office.App.Contracts;
using Office.App.Dto;
using Office.Services.Contracts;

namespace Office.App.Commands
{
    public class CreateTeamCommand : ICommand
    {
        private const string SuccessMessage = "Team {0} successfully created!";
        private const string TeamExists = "Team [team] exists!";
        private const string InvalidAcronym = "Acronym [acronym] not valid!";
        private const string NoUser = "You should login first!";
        private const string InvalidDetails = "Invalid details!";
        private const string InvalidLength = "Invalid arguments count!";

        private readonly ITeamService teamService;


        public CreateTeamCommand(ITeamService teamService)
        {
            this.teamService = teamService;
        }

        public string Execute(string[] data)
        {
            if (!Validation.CheckLength(3, data) && !Validation.CheckLength(2, data))
            {
                return InvalidLength;
            }

            if (Session.User == null)
            {
                return NoUser;

            }
            var dto = new CreateTeamDto
            {
                Name = data[0],
                Acronym = data[1],
             };

            if (data.Length > 2)
            {
                dto.Description = data[2];
            }
           
            if (teamService.Exists(dto.Name))
            {

                return String.Format(TeamExists, dto.Name);

            }

            if (!Validation.ValidateAcronym(dto.Acronym))
            {
                return InvalidAcronym;
            }

            if (!Validation.IsValid(dto))
            {
                return InvalidDetails;
            }

            teamService.CreateTeam(dto.Name, dto.Acronym, dto.Description, Session.User.Username);

            return String.Format(SuccessMessage, dto.Name);

        }
    }
}
