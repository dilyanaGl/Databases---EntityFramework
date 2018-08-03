namespace BusTicketSystem.Service.Contracts
{
    public interface ICompanyService
    {
        TModel ByName<TModel>(string name);
        bool Exists(string name);
        TModel ById<TModel>(int id);
        bool Exists(int id);
    }
}