namespace BusTicketSystem.Service.Contracts
{
    public interface ITicketService
    {
        string BuyTicket(int customerId, int tripId, decimal price, string seat);
        TModel ById<TModel>(int id);
        bool Exists(int id);
    }
}