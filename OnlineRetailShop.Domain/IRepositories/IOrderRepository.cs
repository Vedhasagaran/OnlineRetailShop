using OnlineRetailShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineRetailShop.Domain.Interfaces
{
    public interface IOrderRepository
    {
        Task<Order> AddAsync(Order order);

        Task<Order> CancelAsync(Guid orderId);

        Task<Order> GetByIdAsync(Guid orderId);
    }
}
