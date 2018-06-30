using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetClinic.Models;

namespace PetClinic.Data.Configurations
{
    class AnimalConfiguration : IEntityTypeConfiguration<Animal>
    {
        public void Configure(EntityTypeBuilder<Animal> builder)
        {
            builder.HasKey(p => p.Id);

            builder.HasOne(p => p.Passport)
                .WithOne(p => p.Animal)
            .HasForeignKey<Animal>(p => p.PassportSerialNumber);

        }
    }
}
