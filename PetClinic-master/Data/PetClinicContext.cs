﻿using PetClinic.Data.Configurations;
using PetClinic.Models;

namespace PetClinic.Data
{
    using Microsoft.EntityFrameworkCore;

    public class PetClinicContext : DbContext
    {
        public DbSet<Animal> Animals { get; set; }
        public DbSet<Vet> Vets { get; set; }
        public DbSet<ProcedureAnimalAid> ProceduresAnimalAids { get; set; }
        public DbSet<AnimalAid> AnimalAids { get; set; }
        public DbSet<Procedure> Procedures { get; set; }
        public DbSet<Passport> Passports { get; set; }


        public PetClinicContext()
        {
        }

        public PetClinicContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new AnimalConfiguration());
            builder.ApplyConfiguration(new AnimalAidConfiguration());
            builder.ApplyConfiguration(new VetConfiguration());
            builder.ApplyConfiguration(new ProcedureConfiguration());
            builder.ApplyConfiguration(new ProcedureAnimalAidConfiguration());
            builder.ApplyConfiguration(new PassportConfiguration());
        }
    }
}