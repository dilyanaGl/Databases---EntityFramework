using FastFood.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FastFood.Data.Configurations
{
    internal class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasKey(p => new {p.ItemId, p.OrderId});

            builder.HasOne(p => p.Order)
                .WithMany(p => p.OrderItems)
                .HasForeignKey(p => p.OrderId);

            builder.HasOne(p => p.Item)
                .WithMany(p => p.OrderItems)
                .HasForeignKey(p => p.ItemId);
        }
    }
}