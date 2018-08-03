namespace PhotoShare.Services.Contracts
{
    using Models;

    public interface IUserService
    {
        TModel ById<TModel>(int id);

        TModel ByUsername<TModel>(string username);

        TModel ByUsernameAndPassword<TModel>(string username, string password);

        bool Exists(int id);

        bool Exists(string name);

        User Register(string username, string password, string email);

        void Delete(string username);

        Friendship AddFriend(int userId, int friendId);

        Friendship AcceptFriend(int invited, int invitor);

        void ChangePassword(int userId, string password);

        void SetBornTown(int userId, string townName);

        void SetCurrentTown(int userId, string townName);

        string ListFriends(int userId);

        
    }
}
