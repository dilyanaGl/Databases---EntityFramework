using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using BusTicketSystem.Client.Contracts;

namespace BusTicketSystem.Client
{
    public class CommandInterpreter : ICommandInterpreter
    {
        private const string InvalidCommandName = "Invalid command name!";
        private const string InvalidCommandArgs = "Invalid command args!";
        private const string MissingService = "Missing service!";

        private readonly IServiceProvider provider;

        public CommandInterpreter(IServiceProvider provider)
        {
            this.provider = provider;
        }

        public string ReadCommand(string[] args)
        {
            var commandName = args[0].Split('-').ToArray();
            var commandArgs = args.Skip(1).ToArray();

            var commandType = Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .FirstOrDefault(p => p.Name.Equals($"{String.Join("", commandName)}Command", StringComparison.InvariantCultureIgnoreCase));
            if (commandType == null)
            {
                return InvalidCommandName;
            }

            var constructor = commandType.GetConstructors().First();

            var parameters = constructor
                .GetParameters()
                .Select(p => p.ParameterType);

            var services = parameters
                .Select(this.provider.GetService)
                .ToArray();

            ICommand command;
            try
            {
                command = (ICommand) constructor.Invoke(services);
            }
            catch (Exception e)
            {
                return e.Message;
            }

            try
            {
                return command.Execute(commandArgs);
            }
            catch(Exception e)
            {
                return InvalidCommandArgs + " " + e.Message + " " + e.InnerException;
            }

        }
    }
}
