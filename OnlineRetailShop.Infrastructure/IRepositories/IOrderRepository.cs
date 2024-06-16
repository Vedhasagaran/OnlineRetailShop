using OnlineRetailShop.Domain.DTO;

namespace OnlineRetailShop.Domain.Interfaces
{
    public interface IOrderRepository
    {
        Task<OrderDto> AddAsync(AddOrderRequestDto addOrderRequestDto);

        Task CancelAsync(Guid orderId);

        Task<OrderDto> GetByIdAsync(Guid orderId);
    }
}
