using Office.Models;

namespace Office.Services.Contracts
{
    public interface ITeamEventService
    {
        void AddTeamTo(string eventName, string teamName);
        TeamEvent ByEventAndTeamName(string eventName, string teamName);
        bool Exist(string eventName, string teamName);
    }
}