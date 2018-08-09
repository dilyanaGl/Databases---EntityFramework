using Office.Models;

namespace Office.Services.Contracts
{
    public interface ITeamService
    {
        Team ByName(string name);
        bool Exists(string name);
        void CreateTeam(string name, string acronym, string description, string username);
        void Delete(string name);
        bool IsCreator(string teamName, string username);
        bool IsTeamMember(string teamName, string username);
        string ShowTeam(string teamName);
    }
}