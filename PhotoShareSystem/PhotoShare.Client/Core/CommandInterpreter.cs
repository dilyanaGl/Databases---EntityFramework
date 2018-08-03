namespace PhotoShare.Client.Core
{
    using System;
    using System.Linq;
    using System.Reflection;

    using Client.Core.Contracts;

    public class CommandInterpreter : ICommandInterpreter
    {
        private const string invalidCommandArgs = "Command {0} not valid!";

        private readonly IServiceProvider serviceProvider;

        public CommandInterpreter(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public string Read(string[] input)
        {
            string inputCommand = input[0] + "Command";

            string[] args = input.Skip(1).ToArray();

            var type = Assembly.GetCallingAssembly()
                               .GetTypes()
                               .FirstOrDefault(x => x.Name == inputCommand);

            if (type == null)
            {
                throw new InvalidOperationException("Invalid command!");
            }

            var constructor = type.GetConstructors()
                                  .First();

            var constructorParameters = constructor.GetParameters()
                                                   .Select(x => x.ParameterType)
                                                   .ToArray();

            var service = constructorParameters.Select(serviceProvider.GetService)
                                               .ToArray();

            ICommand command;
            string result = String.Empty;
            try
            {

                command = (ICommand) constructor.Invoke(service);
                result = command.Execute(args);

            }
            catch(Exception ex)
            {
                result = String.Format(invalidCommandArgs, inputCommand);
            }

            return result;
        }
    }
}
