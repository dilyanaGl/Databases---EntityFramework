using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetClinic.Models;

namespace PetClinic.Data.Configurations
{ 
    public class AnimalAidConfiguration : IEntityTypeConfiguration<AnimalAid>
    {
        public void Configure(EntityTypeBuilder<AnimalAid> builder)
        {
            builder.HasKey(p => p.Id);

            builder.HasAlternateKey(p => p.Name);

        }
    }
}
