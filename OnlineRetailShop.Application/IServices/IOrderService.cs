using OnlineRetailShop.Domain.DTO;

namespace OnlineRetailShop.Application.Interfaces
{
    public interface IOrderService
    {
        Task<OrderDto> PlaceOrderAsync(AddOrderRequestDto addOrderRequestDto);

        Task CancelOrderAsync(Guid orderId);

        Task<OrderDto> GetOrderByIdAsync(Guid orderId);
    }
}
