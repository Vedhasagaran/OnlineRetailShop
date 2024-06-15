using Microsoft.EntityFrameworkCore;
using OnlineRetailShop.Domain.Interfaces;
using OnlineRetailShop.Domain.Models;
using OnlineRetailShop.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineRetailShop.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OnlineRetailShopDbContext _onlineRetailShopDbContext;

        public OrderRepository(OnlineRetailShopDbContext onlineRetailShopDbContext)
        {
            _onlineRetailShopDbContext = onlineRetailShopDbContext;
        }

        public async Task<Order> AddAsync(Order order)
        {
            await _onlineRetailShopDbContext.AddAsync(order);
            await _onlineRetailShopDbContext.SaveChangesAsync();
            return order;
        }

        public async Task<Order> CancelAsync(Guid orderId)
        {
            var order = await _onlineRetailShopDbContext.Orders.FindAsync(orderId);
            if (order == null)
            {
                return null;
            }
            _onlineRetailShopDbContext.Orders.Remove(order);
            await _onlineRetailShopDbContext.SaveChangesAsync();
            return order;
        }

        public async Task<Order> GetByIdAsync(Guid orderId)
        {
           // var order = await _onlineRetailShopDbContext.Orders.FindAsync(orderId);
           var order = await _onlineRetailShopDbContext.Orders.Include("Product").FirstOrDefaultAsync(n => n.Id == orderId);
            if (order == null)
                return null;
            return order;
        }
    }
}
