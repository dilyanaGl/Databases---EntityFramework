using System;
using System.Linq;
using BusTicketSystem.Data;
using BusTicketSystem.Service;
using BusTicketSystem.Service.Contracts;
using DataGenerator;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BusTicketSystem.Client
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            ResetDatabase();

            IServiceProvider provider = ConfigureServices();
            CommandInterpreter interpreter = new CommandInterpreter(provider);

            while (true)
            {
                var commandArgs = Console.ReadLine().Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries).ToArray();

                var result = interpreter.ReadCommand(commandArgs);

                Console.WriteLine(result);
            }


        }

        private static IServiceProvider ConfigureServices()
        {
            ServiceCollection services = new ServiceCollection();

            services.AddDbContext<BusTicketContext>();
            services.AddAutoMapper(cfg => cfg.AddProfile<BusTicketProfile>());

            services.AddTransient<ICustomerService, CustomerService>();
            services.AddTransient<ITicketService, TicketService>();
            services.AddTransient<ICompanyService, CompanyService>();
            services.AddTransient<IReviewService, ReviewService>();
            services.AddTransient<IBusStationService, BusStationService>();
            services.AddTransient<ITripService, TripService>();


            IServiceProvider provider = services.BuildServiceProvider();
            return provider;
        }

        private static void ResetDatabase()
        {
            using (var db = new BusTicketContext())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();

                TownGenerator.GenerateTowns(db);
                BusCompanyGenerator.GenerateCompaines(db);
                BusStationGenerator.GenerateBusStations(db);
                CustomerGenerator.GenerateCustomers(db);
                TripGenerator.GenerateTrips(db);
                BankAccountGenerator.GenerateBankAccounts(db);
                ReviewGenerator.GenerateReview(db);
                TicketGenrator.GenerateTickets(db);
            } 
        }
    }
}
