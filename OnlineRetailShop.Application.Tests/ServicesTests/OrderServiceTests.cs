using Moq;
using OnlineRetailShop.Application.Services;
using OnlineRetailShop.Domain.Interfaces;
using OnlineRetailShop.Domain.Models;
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
        private readonly OrderService _orderService;

        public OrderServiceTests()
        {
            _mockOrderRepository = new Mock<IOrderRepository>();
            _mockProductRepository = new Mock<IProductRepository>();
            _orderService = new OrderService(_mockOrderRepository.Object, _mockProductRepository.Object);
        }

        [Fact]
        public async Task PlaceOrderAsync_PlacesOrderAndUpdatesProductQuantity()
        {
            // Arrange
            var order = new Order
            {
                Id = Guid.NewGuid(),
                ProductId = Guid.NewGuid(),
                Quantity = 2,
                OrderDate = DateTime.Now
            };
            var product = new Product
            {
                Id = order.ProductId,
                Name = "Test Product",
                Price = 9.99m,
                Quantity = 10
            };
            _mockProductRepository.Setup(x => x.GetByIdAsync(order.ProductId)).ReturnsAsync(product);
            _mockOrderRepository.Setup(x => x.AddAsync(order)).ReturnsAsync(order);

            // Act
            var result = await _orderService.PlaceOrderAsync(order);

            // Assert
            Assert.Equal(8, product.Quantity);
            Assert.Equal(order, result);
        }

        /*[Fact]
        public async Task PlaceOrderAsync_ThrowsException_WhenProductIsUnavailable()
        {
            // Arrange
            var order = new Order
            {
                Id = Guid.NewGuid(),
                ProductId = Guid.NewGuid(),
                Quantity = 2,
                OrderDate = DateTime.Now
            };
            _mockProductRepository.Setup(x => x.GetByIdAsync(order.ProductId)).ReturnsAsync((Product)null);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _orderService.PlaceOrderAsync(order));
        }*/

        [Fact]
        public async Task PlaceOrderAsync_ThrowsException_WhenProductQuantityIsInsufficient()
        {
            // Arrange
            var order = new Order
            {
                Id = Guid.NewGuid(),
                ProductId = Guid.NewGuid(),
                Quantity = 20,
                OrderDate = DateTime.Now
            };
            var product = new Product
            {
                Id = order.ProductId,
                Name = "Test Product",
                Price = 9.99m,
                Quantity = 10
            };
            _mockProductRepository.Setup(x => x.GetByIdAsync(order.ProductId)).ReturnsAsync(product);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _orderService.PlaceOrderAsync(order));
        }

        [Fact]
        public async Task CancelOrderAsync_CancelsOrderAndUpdatesProductQuantity()
        {
            // Arrange
            var orderId = Guid.NewGuid();
            var order = new Order
            {
                Id = orderId,
                ProductId = Guid.NewGuid(),
                Quantity = 2,
                OrderDate = DateTime.Now
            };
            var product = new Product
            {
                Id = order.ProductId,
                Name = "Test Product",
                Price = 9.99m,
                Quantity = 8
            };
            _mockOrderRepository.Setup(x => x.GetByIdAsync(orderId)).ReturnsAsync(order);
            _mockProductRepository.Setup(x => x.GetByIdAsync(order.ProductId)).ReturnsAsync(product);
            _mockOrderRepository.Setup(x => x.CancelAsync(orderId)).ReturnsAsync(order);

            // Act
            var result = await _orderService.CancelOrderAsync(orderId);

            // Assert
            Assert.Equal(10, product.Quantity);
            Assert.Equal(order, result);
        }

        /*[Fact]
        public async Task CancelOrderAsync_ReturnsNull_WhenOrderIsNotFound()
        {
            // Arrange
            var orderId = Guid.NewGuid();
            _mockOrderRepository.Setup(x => x.GetByIdAsync(orderId)).ReturnsAsync((Order)null);

            // Act
            var result = await _orderService.CancelOrderAsync(orderId);

            // Assert
            Assert.Null(result);
        }*/
    }
}
