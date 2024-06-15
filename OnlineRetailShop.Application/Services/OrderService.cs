using AutoMapper;
using OnlineRetailShop.Application.Interfaces;
using OnlineRetailShop.Domain.DTO;
using OnlineRetailShop.Domain.Interfaces;
using OnlineRetailShop.Utilities.Exceptions;

namespace OnlineRetailShop.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public OrderService(IOrderRepository orderRepository,IProductRepository productRepository,IMapper mapper)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<OrderDto> PlaceOrderAsync(AddOrderRequestDto addOrderRequestDto)
        {
            var product = await _productRepository.GetByIdAsync(addOrderRequestDto.ProductId);
            if (product == null || product.Quantity < addOrderRequestDto.Quantity)
            {
                throw new Exception("Product is unavailable and available quantity is " + product.Quantity);
            }
            
            product.Quantity -= addOrderRequestDto.Quantity;
            var productrequestDto = _mapper.Map<ProductRequestDto>(product);
            await _productRepository.UpdateAsync(product.Id, productrequestDto);            

            return await _orderRepository.AddAsync(addOrderRequestDto);
        }

        public async Task CancelOrderAsync(Guid orderId)
        {
            var order = await _orderRepository.GetByIdAsync(orderId);
            if (order == null)
            {
                 throw new NotFoundException("Order not found");                
            }

            var product = await _productRepository.GetByIdAsync(order.ProductId);
            product.Quantity += order.Quantity;
            var productrequestDto = _mapper.Map<ProductRequestDto>(product);
            await _productRepository.UpdateAsync(product.Id, productrequestDto);           

            await _orderRepository.CancelAsync(orderId);            
        }

        public async Task<OrderDto> GetOrderByIdAsync(Guid orderId)
        {
            var order = await _orderRepository.GetByIdAsync(orderId); 

            return _mapper.Map<OrderDto>(order);
        }
    }
}
