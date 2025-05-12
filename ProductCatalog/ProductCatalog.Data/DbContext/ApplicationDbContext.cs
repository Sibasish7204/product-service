using Microsoft.EntityFrameworkCore;
using ProductCatalog.Data.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalog.Data.DbContext
{
    public class ApplicationDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ShoppingCart>()
                .HasOne(s => s.Customer)
                .WithMany(c => c.ShoppingCarts)
                .HasForeignKey(s => s.CustomerId);

            modelBuilder.Entity<ShoppingCart>()
                .HasOne(s => s.Product)
                .WithMany(p => p.ShoppingCarts)
                .HasForeignKey(s => s.ProductId);
        }
    }
}

