using Microsoft.EntityFrameworkCore;
using OnlineRetailShop.Domain.Models;

namespace OnlineRetailShop.Infrastructure.Data
{
    public class OnlineRetailShopDbContext : DbContext
    {
        public OnlineRetailShopDbContext(DbContextOptions<OnlineRetailShopDbContext> options)
            : base(options)
        {

        }

        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            //Seed Data for Products
            var products = new List<Product>()
            {
                new() 
                {
                    Id = Guid.Parse("f974bc9e-fbee-4704-baf4-0c175ba56381"),
                    Name = "Appple",
                    Price = 1200m,
                    Quantity = 100
                },
                new() 
                {
                    Id = Guid.Parse("5ee17da1-281a-48de-a16d-b6d8d25c64bf"),
                    Name = "Orange",
                    Price = 200m,
                    Quantity = 200
                }
            };

            //Seed Products into Database
            modelBuilder.Entity<Product>().HasData(products);

            //Seed Data for Orders
            var orders = new List<Order>()
            {
                new()
                {
                    Id = Guid.NewGuid(),
                    ProductId = new Guid("f974bc9e-fbee-4704-baf4-0c175ba56381"), // Ensure this matches a Product Id
                    Quantity = 10,
                    OrderDate = DateTime.Now
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    ProductId = new Guid("5ee17da1-281a-48de-a16d-b6d8d25c64bf"), // Ensure this matches a Product Id
                    Quantity = 20,
                    OrderDate = DateTime.Now
                }
            };

            //Seed Orders to Database
            modelBuilder.Entity<Order>().HasData(orders);

        }
    }
}
