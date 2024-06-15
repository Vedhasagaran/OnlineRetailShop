using OnlineRetailShop.Application.Interfaces;
using OnlineRetailShop.Domain.Interfaces;
using OnlineRetailShop.Domain.Models;
using OnlineRetailShop.Infrastructure.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineRetailShop.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;

        public OrderService(IOrderRepository orderRepository,IProductRepository productRepository)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
        }

        public async Task<Order> PlaceOrderAsync(Order order)
        {
            var product = await _productRepository.GetByIdAsync(order.ProductId);
            if (product == null || product.Quantity < order.Quantity)
            {
                throw new Exception("Product is unavailable and available quantity is " + product.Quantity);
            }
            
            product.Quantity -= order.Quantity;
            await _productRepository.UpdateAsync(product.Id,product);

            return await _orderRepository.AddAsync(order);
        }

        public async Task<Order> CancelOrderAsync(Guid orderId)
        {
            var order = await _orderRepository.GetByIdAsync(orderId);
            if (order == null)
            {
                 throw new NotFoundException("Order not found");
                //return null;
            }

            var product = await _productRepository.GetByIdAsync(order.ProductId);
            product.Quantity += order.Quantity;
            await _productRepository.UpdateAsync(product.Id,product);

            await _orderRepository.CancelAsync(orderId);
            return order;
        }

        public async Task<Order> GetOrderByIdAsync(Guid orderId)
        {
            return await _orderRepository.GetByIdAsync(orderId); 
        }
    }
}
