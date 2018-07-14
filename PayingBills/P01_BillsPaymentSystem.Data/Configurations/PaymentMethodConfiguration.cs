using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P01_BillsPaymentSystem.Data.Models;

namespace P01_BillsPaymentSystem.Data.Configurations
{
    public class PaymentMethodConfiguration : IEntityTypeConfiguration<PaymentMethod>
    {
        public void Configure(EntityTypeBuilder<PaymentMethod> builder)
        {
            builder.HasKey(p => p.Id);

            builder.HasIndex(p => new {p.BankAccountId, p.CreditCardId, p.UserId})
                .IsUnique();

            builder.HasOne(p => p.BankAccount)
                .WithMany(p => p.PaymentMethods)
                .HasForeignKey(p => p.BankAccountId);

            builder.HasOne(p => p.User)
                .WithMany(p => p.PaymentMethods)
                .HasForeignKey(p => p.UserId);

            builder.HasOne(p => p.CreditCard)
                .WithMany(p => p.PaymentMethods)
                .HasForeignKey(p => p.CreditCardId);
        }
    }
}