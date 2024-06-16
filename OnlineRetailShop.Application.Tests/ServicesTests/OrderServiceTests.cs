using AutoMapper;
using Moq;
using OnlineRetailShop.Application.Services;
using OnlineRetailShop.Domain.DTO;
using OnlineRetailShop.Domain.Interfaces;
using OnlineRetailShop.Domain.Models;
using OnlineRetailShop.Utilities.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineRetailShop.Application.Tests.ServicesTests
{
    public class OrderServiceTests
    {
        private readonly Mock<IOrderRepository> _mockOrderRepository;
        private readonly Mock<IProductRepository> _mockProductRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly OrderService _orderService;

        public OrderServiceTests()
        {
            _mockOrderRepository = new Mock<IOrderRepository>();
            _mockProductRepository = new Mock<IProductRepository>();
            _mockMapper = new Mock<IMapper>();
            _orderService = new OrderService(_mockOrderRepository.Object, _mockProductRepository.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task PlaceOrderAsync_ShouldPlaceOrder_WhenProductIsAvailable()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var addOrderRequestDto = new AddOrderRequestDto { ProductId = productId, Quantity = 2 };
            var product = new ProductDto { Id = productId, Name = "Product 1", Price = 10, Quantity = 5 };
            var orderDto = new OrderDto { Id = Guid.NewGuid(), ProductId = productId, Quantity = 2 };

            _mockProductRepository.Setup(repo => repo.GetByIdAsync(productId)).ReturnsAsync(product);
            _mockProductRepository.Setup(repo => repo.UpdateAsync(product.Id, It.IsAny<ProductRequestDto>())).ReturnsAsync(product);
            _mockOrderRepository.Setup(repo => repo.AddAsync(addOrderRequestDto)).ReturnsAsync(orderDto);
            _mockMapper.Setup(mapper => mapper.Map<ProductRequestDto>(product)).Returns(new ProductRequestDto { Name = product.Name, Price = product.Price, Quantity = product.Quantity - addOrderRequestDto.Quantity });

            // Act
            var result = await _orderService.PlaceOrderAsync(addOrderRequestDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(orderDto.Id, result.Id);
            _mockProductRepository.Verify(repo => repo.UpdateAsync(product.Id, It.IsAny<ProductRequestDto>()), Times.Once);
            _mockOrderRepository.Verify(repo => repo.AddAsync(addOrderRequestDto), Times.Once);
        }

        [Fact]
        public async Task PlaceOrderAsync_ShouldThrowException_WhenProductIsUnavailable()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var addOrderRequestDto = new AddOrderRequestDto { ProductId = productId, Quantity = 10 };
            var product = new ProductDto { Id = productId, Name = "Product 1", Price = 10, Quantity = 5 };

            _mockProductRepository.Setup(repo => repo.GetByIdAsync(productId)).ReturnsAsync(product);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => _orderService.PlaceOrderAsync(addOrderRequestDto));
            Assert.Equal("Product is unavailable and available quantity is " + product.Quantity, exception.Message);
        }

        [Fact]
        public async Task CancelOrderAsync_ShouldCancelOrder_WhenOrderExists()
        {
            // Arrange
            var orderId = Guid.NewGuid();
            var productId = Guid.NewGuid();
            var order = new OrderDto { Id = orderId, ProductId = productId, Quantity = 2 };
            var product = new ProductDto { Id = productId, Name = "Product 1", Price = 10, Quantity = 5 };

            _mockOrderRepository.Setup(repo => repo.GetByIdAsync(orderId)).ReturnsAsync(order);
            _mockOrderRepository.Setup(repo => repo.CancelAsync(orderId)).Returns(Task.CompletedTask);
            _mockProductRepository.Setup(repo => repo.GetByIdAsync(productId)).ReturnsAsync(product);
            _mockProductRepository.Setup(repo => repo.UpdateAsync(product.Id, It.IsAny<ProductRequestDto>())).ReturnsAsync(product);
            _mockMapper.Setup(mapper => mapper.Map<ProductRequestDto>(product)).Returns(new ProductRequestDto { Name = product.Name, Price = product.Price, Quantity = product.Quantity + order.Quantity });

            // Act
            await _orderService.CancelOrderAsync(orderId);

            // Assert
            _mockOrderRepository.Verify(repo => repo.CancelAsync(orderId), Times.Once);
            _mockProductRepository.Verify(repo => repo.UpdateAsync(product.Id, It.IsAny<ProductRequestDto>()), Times.Once);
        }

        [Fact]
        public async Task CancelOrderAsync_ShouldThrowNotFoundException_WhenOrderDoesNotExist()
        {
            // Arrange
            var orderId = Guid.NewGuid();

            _mockOrderRepository.Setup(repo => repo.GetByIdAsync(orderId)).ReturnsAsync((OrderDto)null);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<NotFoundException>(() => _orderService.CancelOrderAsync(orderId));
            Assert.Equal("Order not found", exception.Message);
        }
       
        [Fact]
        public async Task GetOrderByIdAsync_ShouldReturnNull_WhenOrderDoesNotExist()
        {
            // Arrange
            _mockOrderRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((OrderDto)null);

            // Act
            var result = await _orderService.GetOrderByIdAsync(Guid.NewGuid());

            // Assert
            Assert.Null(result);
        }
    }
}
