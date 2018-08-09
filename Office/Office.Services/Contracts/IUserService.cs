using Office.Models;

namespace Office.Services.Contracts
{
    public interface IUserService
    {
        void AddUser(string username, string password, string firstName, string lastName, int age, string genderString);
        TModel ById<TModel>(int id);
        User ByUsername(string username);
        void Delete(string username);
        bool Exist(string username);
        bool Exist(string username, string password);
        User GetsUserByUsernameAndPassword(string username, string password);
    }
}