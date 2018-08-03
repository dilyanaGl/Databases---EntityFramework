using System;
using System.Collections.Generic;
using System.Text;
using BusTicketSystem.Client.Contracts;

namespace BusTicketSystem.Client.Command
{
    public class ExitCommand : ICommand
    {
        public string Execute(string[] data)
        {
            Environment.Exit(0);
            return "Bye-bye!";
        }
    }
}
