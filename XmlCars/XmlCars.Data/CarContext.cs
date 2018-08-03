using System;
using Microsoft.EntityFrameworkCore;
using XmlCars.Models;

namespace XmlCars.Data
{
    public class CarContext : DbContext
    {
        public DbSet<Part> Parts { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<PartCar> PartCars { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
            {
                builder.UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Car>()
                .HasKey(p => p.Id);

            builder.Entity<Sale>()
                .HasKey(p => p.Id);

            builder.Entity<Sale>()
                .HasOne(p => p.Car)
                .WithMany(p => p.Sales)
                .HasForeignKey(p => p.CarId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Sale>()
                .HasOne(p => p.Customer)
                .WithMany(p => p.Sales)
                .HasForeignKey(p => p.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Customer>()
                .HasKey(p => p.Id);

            builder.Entity<Part>()
                .HasKey(p => p.Id);

            builder.Entity<Part>()
                .HasOne(p => p.Supplier)
                .WithMany(p => p.Parts)
                .HasForeignKey(p => p.SupplierId)
                .OnDelete(DeleteBehavior.Restrict);


            builder.Entity<PartCar>()
                .HasKey(p => new { p.PartId, p.CarId });

            builder.Entity<PartCar>()
                .HasOne(p => p.Car)
                .WithMany(p => p.Parts)
                .HasForeignKey(p => p.CarId)
                .OnDelete(DeleteBehavior.Restrict);


            builder.Entity<PartCar>()
                .HasOne(p => p.Part)
                .WithMany(p => p.Cars)
                .HasForeignKey(p => p.PartId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
