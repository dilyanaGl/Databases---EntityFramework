namespace BusTicketSystem.Client.Contracts
{
    public interface ICommandInterpreter
    {
        string ReadCommand(string[] args);
    }
}