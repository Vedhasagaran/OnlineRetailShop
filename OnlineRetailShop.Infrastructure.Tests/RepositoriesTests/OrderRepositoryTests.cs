using AutoMapper;
using EntityFrameworkCoreMock;
using Microsoft.EntityFrameworkCore;
using Moq;
using OnlineRetailShop.Domain.DTO;
using OnlineRetailShop.Domain.Models;
using OnlineRetailShop.Infrastructure.Data;
using OnlineRetailShop.Infrastructure.Repositories;
using OnlineRetailShop.Utilities.Exceptions;

namespace OnlineRetailShop.Infrastructure.Tests.RepositoriesTests
{
    public class OrderRepositoryTests
    {
        private readonly Mock<IMapper> _mockMapper;
        private readonly OrderRepository _repository;
        private readonly DbContextMock<OnlineRetailShopDbContext> _mockDbContext;

        public OrderRepositoryTests()
        {
            var ordersInitialData = new List<Order>
            {
                new Order { Id = Guid.NewGuid(), ProductId = Guid.NewGuid(), Quantity = 1, OrderDate = DateTime.Now },
                new Order { Id = Guid.NewGuid(), ProductId = Guid.NewGuid(), Quantity = 2, OrderDate = DateTime.Now }
            };

            _mockDbContext = new DbContextMock<OnlineRetailShopDbContext>(new DbContextOptionsBuilder<OnlineRetailShopDbContext>().Options);
            _mockDbContext.CreateDbSetMock(context => context.Orders, ordersInitialData);

            _mockMapper = new Mock<IMapper>();
            _repository = new OrderRepository(_mockDbContext.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnOrder_WhenOrderExists()
        {
            // Arrange
            var orderId = _mockDbContext.Object.Orders.First().Id;
            var orderDto = new OrderDto { Id = orderId, ProductId = Guid.NewGuid(), Quantity = 1, OrderDate = DateTime.Now };
            _mockMapper.Setup(m => m.Map<OrderDto>(It.IsAny<Order>())).Returns(orderDto);

            // Act
            var result = await _repository.GetByIdAsync(orderId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(orderId, result.Id);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnNull_WhenOrderDoesNotExist()
        {
            // Arrange

            // Act
            var result = await _repository.GetByIdAsync(Guid.NewGuid());

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task AddAsync_ShouldAddOrder()
        {
            // Arrange
            var addOrderRequestDto = new AddOrderRequestDto { ProductId = Guid.NewGuid(), Quantity = 3 };
            var order = new Order { Id = Guid.NewGuid(), ProductId = addOrderRequestDto.ProductId, Quantity = addOrderRequestDto.Quantity, OrderDate = DateTime.Now };

            _mockMapper.Setup(m => m.Map<Order>(addOrderRequestDto)).Returns(order);
            _mockMapper.Setup(m => m.Map<OrderDto>(order)).Returns(new OrderDto
            {
                Id = order.Id,
                ProductId = order.ProductId,
                Quantity = order.Quantity,
                OrderDate = order.OrderDate
            });

            // Act
            var result = await _repository.AddAsync(addOrderRequestDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(order.ProductId, result.ProductId);
        }

        [Fact]
        public async Task CancelAsync_ShouldDeleteOrder()
        {
            // Arrange
            var orderId = _mockDbContext.Object.Orders.First().Id;

            // Act
            await _repository.CancelAsync(orderId);

            // Assert
            Assert.Null(_mockDbContext.Object.Orders.FirstOrDefault(o => o.Id == orderId));
        }

        [Fact]
        public async Task CancelAsync_ShouldThrowNotFoundException_WhenOrderDoesNotExist()
        {
            // Arrange

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => _repository.CancelAsync(Guid.NewGuid()));
        }
    }
}
