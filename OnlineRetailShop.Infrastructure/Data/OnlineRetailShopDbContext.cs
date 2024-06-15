using Microsoft.EntityFrameworkCore;
using OnlineRetailShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineRetailShop.Infrastructure.Data
{
    public class OnlineRetailShopDbContext : DbContext
    {
        public OnlineRetailShopDbContext(DbContextOptions<OnlineRetailShopDbContext> options)
            : base(options)
        {
                
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*modelBuilder.Entity<Product>()
               .Property(p => p.Price)
               .HasColumnType("decimal(18,2)");*/


            base.OnModelCreating(modelBuilder);           
        }
    }
}
