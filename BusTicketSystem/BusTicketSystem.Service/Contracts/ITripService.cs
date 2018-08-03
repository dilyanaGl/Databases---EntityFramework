namespace BusTicketSystem.Service.Contracts
{
    public interface ITripService
    {
        TModel ById<TModel>(int id);
        string ChangeTripStatus(int id, string statusString);
        bool Exists(int id);
    }
}