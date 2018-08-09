using Office.Models;

namespace Office.Services.Contracts
{
    public interface IInvitationService
    {
        void AcceptInvite(string teamname, string username);
        Invitation ByTeamAndUserName(string username, string teamName);
        void DeclineInvite(int inviteId);
        bool Exists(string username, string teamName);
        void InviteUser(string teamName, string username);
    }
}