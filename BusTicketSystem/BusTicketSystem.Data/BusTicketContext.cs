using System;
using System.Collections.Generic;
using System.Text;
using BusTicketSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace BusTicketSystem.Data
{
    public class BusTicketContext : DbContext
    {

        public DbSet<Town> Towns { get; set; }

        public DbSet<BusCompany> BusCompanies { get; set; }

        public DbSet<Ticket> Tickets { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Trip> Trips { get; set; }

        public DbSet<BusStation> BusStations { get; set; }

        public DbSet<Review> Reviews { get; set; }

        public DbSet<BankAccount> BankAccounts { get; set; }

        public DbSet<ArrivedTrip> ArrivedTrips { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
            {
                builder.UseSqlServer(Configuration.ConnetctionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Town>()
                .HasKey(p => p.Id);

            builder.Entity<BusCompany>()
                .HasKey(p => p.Id);

            builder.Entity<BusStation>()
                .HasKey(p => p.Id);

            builder.Entity<Trip>()
                .HasKey(p => p.Id);

            builder.Entity<Trip>()
                .HasOne(p => p.OriginStation)
                .WithMany(p => p.OriginTrips)
                .HasForeignKey(p => p.OriginStationId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Trip>()
                .HasOne(p => p.DestinationStation)
                .WithMany(p => p.DestinationTrips)
                .HasForeignKey(p => p.DestinationStationId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Trip>()
                .HasOne(p => p.BusCompany)
                .WithMany(p => p.Trips)
                .HasForeignKey(p => p.BusCompanyId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Customer>()
                .HasOne(p => p.HomeTown)
                .WithMany(p => p.Citizens)
                .HasForeignKey(p => p.HomeTownId)
                .OnDelete(DeleteBehavior.Restrict);


            builder.Entity<Ticket>()
                .HasKey(p => p.Id);

            builder.Entity<Ticket>()
                .HasOne(p => p.Customer)
                .WithMany(p => p.Tickets)
                .HasForeignKey(p => p.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Ticket>()
                .HasOne(p => p.Trip)
                .WithMany(p => p.Tickets)
                .HasForeignKey(p => p.TripId)
                .OnDelete(DeleteBehavior.Restrict);


            builder.Entity<BusStation>()
                .HasKey(p => p.Id);

            builder.Entity<BusStation>()
                .HasOne(p => p.Town)
                .WithMany(p => p.BusStations)
                .HasForeignKey(p => p.TownId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Review>()
                .HasKey(p => p.Id);

            builder.Entity<Review>()
                .HasOne(p => p.Customer)
                .WithMany(p => p.Reviews)
                .HasForeignKey(p => p.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Review>()
                .HasOne(p => p.BusCompany)
                .WithMany(p => p.Reviews)
                .HasForeignKey(p => p.BusCompanyId)
                .OnDelete(DeleteBehavior.Restrict);


            builder.Entity<BankAccount>()
                .HasKey(p => p.Id);

            builder.Entity<BankAccount>()
                .HasOne(p => p.Customer)
                .WithOne(p => p.BankAccount)
                .HasForeignKey<BankAccount>(p => p.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ArrivedTrip>()
                .HasKey(p => p.Id);

            builder.Entity<ArrivedTrip>()
                .HasOne(p => p.OriginStation)
                .WithMany(p => p.ArrivedOriginTrips)
                .HasForeignKey(p => p.OriginStationId)
                .OnDelete(DeleteBehavior.Restrict);


            builder.Entity<ArrivedTrip>()
                .HasOne(p => p.DestinationStation)
                .WithMany(p => p.ArrivedDestinationTrips)
                .HasForeignKey(p => p.DestinationStationId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
