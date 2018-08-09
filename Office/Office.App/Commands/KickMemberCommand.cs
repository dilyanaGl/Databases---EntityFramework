using System;
using System.Collections.Generic;
using System.Text;
using Office.App.Contracts;
using Office.Services.Contracts;

namespace Office.App.Commands
{
    public class KickMemberCommand: ICommand

    {
    private const string SuccessMessage = "User {0} was kicked from {1}!";
    private const string TeamNotFound = "Team {0} not found!";
    private const string UserNotFound = "User {0} not found!";
    private const string NoTeamMember = "User {0} is not a member in {1}!";
    private const string NotAllowed = "Not allowed!";
    private const string CommandNotAllowed = "Command not allowed. Use DisbandTeam instead.";
    private const string NoUser = "You should login first!";
    private const string InvalidLength = "Invalid arguments count!";

    private readonly ITeamService teamService;
    private readonly IUserService userService;

    public KickMemberCommand(ITeamService teamService, IUserService userService)
    {
        this.teamService = teamService;
        this.userService = userService;
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

        string teamName = data[0];
        string username = data[1];

        if (!teamService.Exists(teamName))
        {
            return String.Format(TeamNotFound, teamName);

        }

        if (!userService.Exist(username))
        {

            return String.Format(UserNotFound, username);
        }

        if (!teamService.IsTeamMember(teamName, username))
        {
            return String.Format(NoTeamMember, username, teamName);

        }

        if (!teamService.IsCreator(teamName, Session.User.Username))
        {

            return NotAllowed;

        }

        if (teamService.IsCreator(teamName, username))
        {

            return CommandNotAllowed;
        }


        return String.Format(SuccessMessage, username, teamName);


    }
    }
}
