using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetClinic.Models;

namespace PetClinic.Data.Configurations
{
    public class ProcedureConfiguration : IEntityTypeConfiguration<Procedure>
    {
        public void Configure(EntityTypeBuilder<Procedure> builder)
        {
            builder.HasKey(p => p.Id);

            builder.HasOne(p => p.Animal)
                .WithMany(k => k.Procedures)
                .HasForeignKey(p => p.AnimalId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.Vet)
                .WithMany(k => k.Procedures)
                .HasForeignKey(p => p.VetId)
                .OnDelete(DeleteBehavior.Restrict);


        }
    }
}
