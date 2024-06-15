using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using OnlineRetailShop.Api.Controllers;
using OnlineRetailShop.Application.Interfaces;
using OnlineRetailShop.Domain.DTO;
using OnlineRetailShop.Domain.Mappings;
using OnlineRetailShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineRetailShop.Api.Tests.ControllersTests
{
    public class OrderControllerTests
    {
        private readonly Mock<IOrderService> _mockOrderService;
        private readonly OrderController _controller;
        private readonly IMapper _mapper;

        public OrderControllerTests()
        {
            _mockOrderService = new Mock<IOrderService>();
            _mapper = new Mapper(new MapperConfiguration(configuration =>
                configuration.AddProfile(new AutoMapperProfiles())));
            _controller = new OrderController(_mockOrderService.Object, _mapper);
        }



        #region PlaceOrder
        [Fact]
        public async Task PlaceOrder_ReturnsOkResult_WithPlacedOrder()
        {
            // Arrange
            var order = new Order
            {
                Id = Guid.NewGuid(),
                ProductId = Guid.NewGuid(),
                Quantity = 2,
                OrderDate = DateTime.Now
            };
            _mockOrderService.Setup(x => x.PlaceOrderAsync(It.IsAny<Order>())).ReturnsAsync(order);

            // Act
            var result = await _controller.PlaceOrder(new AddOrderRequestDto
            {
                ProductId = order.ProductId,
                Quantity = order.Quantity,
                OrderDate = order.OrderDate
            });

            // Assert
            Assert.IsType<CreatedAtActionResult>(result);
        }
        #endregion

        #region CancelOrder
        [Fact]
        public async Task CancelOrder_ReturnsOkResult_WithCanceledOrder()
        {
            // Arrange
            var orderId = Guid.NewGuid();
            var canceledOrder = new Order
            {
                Id = orderId,
                ProductId = Guid.NewGuid(),
                Quantity = 2,
                OrderDate = DateTime.Now
            };
            _mockOrderService.Setup(x => x.CancelOrderAsync(orderId)).ReturnsAsync(canceledOrder);

            // Act
            var result = await _controller.CancelOrder(orderId);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }
        #endregion
    }
}
