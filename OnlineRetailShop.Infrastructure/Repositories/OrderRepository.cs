using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineRetailShop.Domain.DTO;
using OnlineRetailShop.Domain.Interfaces;
using OnlineRetailShop.Domain.Models;
using OnlineRetailShop.Infrastructure.Data;
using OnlineRetailShop.Utilities.Exceptions;

namespace OnlineRetailShop.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OnlineRetailShopDbContext _onlineRetailShopDbContext;
        private readonly IMapper _mapper;

        public OrderRepository(OnlineRetailShopDbContext onlineRetailShopDbContext,IMapper mapper)
        {
            _onlineRetailShopDbContext = onlineRetailShopDbContext;
            _mapper = mapper;
        }

        public async Task<OrderDto> GetByIdAsync(Guid orderId)
        {
            var order = await _onlineRetailShopDbContext.Orders.Include("Product").FirstOrDefaultAsync(n => n.Id == orderId);

            if (order == null)
                return null;

            return _mapper.Map<OrderDto>(order);
        }
        public async Task<OrderDto> AddAsync(AddOrderRequestDto addOrderRequestDto)
        {
            var order = _mapper.Map<Order>(addOrderRequestDto);
            await _onlineRetailShopDbContext.AddAsync(order);
            await _onlineRetailShopDbContext.SaveChangesAsync();
            var orderDto = _mapper.Map<OrderDto>(order);
            return orderDto;
        }
        public async Task CancelAsync(Guid orderId)
        {
            var order = await _onlineRetailShopDbContext.Orders.FindAsync(orderId);
            if (order == null)
            {
                throw new NotFoundException("Order not found");
            }
            _onlineRetailShopDbContext.Orders.Remove(order);
            await _onlineRetailShopDbContext.SaveChangesAsync();            
        }

        
    }
}
