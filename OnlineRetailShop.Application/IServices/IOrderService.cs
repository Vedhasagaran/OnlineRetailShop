using OnlineRetailShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineRetailShop.Application.Interfaces
{
    public interface IOrderService
    {
        Task<Order> PlaceOrderAsync(Order order);

        Task<Order> CancelOrderAsync(Guid orderId);

        Task<Order> GetOrderByIdAsync(Guid orderId);


    }
}
