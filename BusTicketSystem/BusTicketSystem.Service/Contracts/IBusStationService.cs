namespace BusTicketSystem.Service.Contracts
{
    public interface IBusStationService
    {
        TModel ById<TModel>(int id);
        bool Exist(int id);
        string PrintInformation(int busStationId);
    }
}