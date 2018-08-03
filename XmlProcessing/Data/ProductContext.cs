using System;
using DataModels;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class ProductContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<CategoryProduct> CategoryProducts { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
            {
                builder.UseSqlServer(Configuration.ConnectionString);
            }

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<Product>()
                .HasKey(p => p.Id);

            builder.Entity<Product>()
                .HasOne(p => p.Buyer)
                .WithMany(p => p.ProductsBought)
                .HasForeignKey(p => p.BuyerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Product>()
                .HasOne(p => p.Seller)
                .WithMany(p => p.ProductsSold)
                .HasForeignKey(p => p.SellerId)
                .OnDelete(DeleteBehavior.Restrict);


            builder.Entity<User>()
                .HasKey(p => p.Id);

            builder.Entity<Category>()
                .HasKey(p => p.Id);

            builder.Entity<CategoryProduct>()
                .HasKey(p => new { p.ProductId, p.CategoryId });

            builder.Entity<CategoryProduct>()
                .HasOne(p => p.Category)
                .WithMany(p => p.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<CategoryProduct>()
                .HasOne(p => p.Product)
                .WithMany(p => p.Categories)
                .HasForeignKey(p => p.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
