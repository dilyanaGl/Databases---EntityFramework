using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetClinic.Models;

namespace PetClinic.Data.Configurations
{
    class ProcedureAnimalAidConfiguration : IEntityTypeConfiguration<ProcedureAnimalAid>
    {
        public void Configure(EntityTypeBuilder<ProcedureAnimalAid> builder)
        {
            builder.HasKey(p => new {p.ProcedureId, p.AnimalAidId});

            builder.HasOne(p => p.Procedure)
                .WithMany(k => k.ProcedureAnimalAids)
                .HasForeignKey(p => p.ProcedureId);

            builder.HasOne(p => p.AnimalAid)
                .WithMany(k => k.AnimalAidProcedures)
                .HasForeignKey(p => p.AnimalAidId);

        }
    }
}
