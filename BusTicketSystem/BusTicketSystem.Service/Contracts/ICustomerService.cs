namespace BusTicketSystem.Service.Contracts
{
    public interface ICustomerService
    {
        bool Exists(int id);

        TModel ById<TModel>(int id);
        void PayTicket(int id, decimal price);
        bool HasEnoughMoney(int id, decimal price);
    }
}
