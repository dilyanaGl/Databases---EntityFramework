using System;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Office.Data;
using Office.Services;
using Office.Services.Contracts;
using AutoMapper;
using Office.App.Contracts;

namespace Office.App
{
    class StartUp
    {
        static void Main(string[] args)
        {
            ResetDatabase();

            IServiceProvider provider = ConfigureServices();
            ICommandInterpreter interpreter = new CommandInterpreter(provider);

            while (true)
            {
                var commandArgs = Console.ReadLine().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToArray();

                var result = interpreter.Read(commandArgs);

                Console.WriteLine(result);
            }
        }

        private static void ResetDatabase()
        {
            using (var db = new OfficeContext())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
            }
        }

        private static IServiceProvider ConfigureServices()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddDbContext<OfficeContext>();
            serviceCollection.AddAutoMapper(cfg => cfg.AddProfile<OfficeProfile>());

            serviceCollection.AddTransient<IEventService, EventService>();
            serviceCollection.AddTransient<ITeamService, TeamService>();
            serviceCollection.AddTransient<IUserService, UserService>();
            serviceCollection.AddTransient<ITeamEventService, TeamEventService>();
            serviceCollection.AddTransient<IInvitationService, InvitationService>();

            var serviceProvider = serviceCollection.BuildServiceProvider();

            return serviceProvider;
        }
    }
}
