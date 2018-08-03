using BusTicketSystem.Client.Contracts;
using BusTicketSystem.Service.Contracts;

namespace BusTicketSystem.Client.Command
{
    public class PrintInfoCommand : ICommand
    {
        private const string BusStationNotFound = "Bus station does not exist";

        private readonly IBusStationService busstationService;

        public PrintInfoCommand(IBusStationService busstationService)
        {
            this.busstationService = busstationService;
        }

        public string Execute(string[] data)
        {
            int id = int.Parse(data[0]);

            if (!busstationService.Exist(id))
            {
                return BusStationNotFound;
            }

            return busstationService.PrintInformation(id);
        }
    }
}
