using Microsoft.EntityFrameworkCore;
using odevSon.Models.ProductOrderManagement.Models;
using System;

namespace odevSon.Models
{
    public class ProductVT : DbContext
    {
        public ProductVT(DbContextOptions<ProductVT> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().Property(p => p.Name).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Product>().Property(p => p.Price).IsRequired();
            modelBuilder.Entity<Product>().Property(p => p.Stock).IsRequired();


            modelBuilder.Entity<Order>()
                        .HasOne(o => o.Product)
                        .WithMany()
                        .HasForeignKey(o => o.ProductId);
            modelBuilder.Entity<Order>().Property(o => o.Quantity).IsRequired();
            modelBuilder.Entity<Order>().Property(o => o.OrderDate).IsRequired();

            modelBuilder.Entity<Customer>().HasKey(c => c.Id);
            modelBuilder.Entity<Customer>()
                        .Property(c => c.Id)
                        .ValueGeneratedOnAdd();
            modelBuilder.Entity<Customer>().Property(c => c.Name).IsRequired();
            modelBuilder.Entity<Customer>().Property(c => c.Email).IsRequired();
        }
    }
}
