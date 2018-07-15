using FastFood.Models;
using FastFood.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FastFood.Data.Configurations
{
    internal class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.DateTime)
                .HasColumnType("DATETIME2");

            //builder.Property(p => p.Type)
            //    .HasDefaultValue(OrderType.ToGo);

            builder.Ignore(p => p.TotalPrice);

            builder.HasOne(p => p.Employee)
                .WithMany(p => p.Orders)
                .HasForeignKey(p => p.EmployeeId);


        }
    }
}