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
    public class ProductControllerTests
    {
        private readonly Mock<IProductService> _mockProductService;
        private readonly ProductController _controller;
        private readonly IMapper _mapper;

        public ProductControllerTests()
        {
            _mockProductService = new Mock<IProductService>();
            _mapper = new Mapper(new MapperConfiguration(configuration =>
                configuration.AddProfile(new AutoMapperProfiles())));
            _controller = new ProductController(_mockProductService.Object, _mapper);
        }
        #region GetAll
        [Fact]
        public async Task GetAll_ReturnsOkResult_WithListOfProducts()
        {
            // Arrange
            var products = new List<Product>
        {
            new Product { Id = Guid.NewGuid(), Name = "Product 1", Price = 10.99m, Quantity = 5 },
            new Product { Id = Guid.NewGuid(), Name = "Product 2", Price = 19.99m, Quantity = 10 }
        };
            _mockProductService.Setup(x => x.GetAll()).ReturnsAsync(products);

            // Act
            var result = await _controller.GetAll();

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }
        #endregion

        #region GetById
        [Fact]
        public async Task GetById_ReturnsOkResult_WithProductDto()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var product = new Product
            {
                Id = productId,
                Name = "Test Product",
                Price = 9.99m,
                Quantity = 10
            };
            _mockProductService.Setup(x => x.GetProductByIdAsync(productId)).ReturnsAsync(product);

            // Act
            var result = await _controller.GetById(productId);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }
        #endregion

        #region AddProduct
        [Fact]
        public async Task AddProduct_ReturnsCreatedAtActionResult_WithProductDto()
        {
            // Arrange
            var productRequestDto = new ProductRequestDto
            {
                Name = "Test Product",
                Price = 9.99m,
                Quantity = 10
            };
            var createdProduct = new Product
            {
                Id = Guid.NewGuid(),
                Name = productRequestDto.Name,
                Price = productRequestDto.Price,
                Quantity = productRequestDto.Quantity
            };
            _mockProductService.Setup(x => x.AddProductAsync(It.IsAny<Product>())).ReturnsAsync(createdProduct);

            // Act
            var result = await _controller.AddProduct(productRequestDto);

            // Assert
            Assert.IsType<CreatedAtActionResult>(result);
        }
        #endregion

        #region UpdateProduct
        [Fact]
        public async Task Update_ReturnsOkResult_WithUpdatedProduct()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var productRequestDto = new ProductRequestDto
            {
                Name = "Updated Product",
                Price = 14.99m,
                Quantity = 20
            };
            var updatedProduct = new Product
            {
                Id = productId,
                Name = productRequestDto.Name,
                Price = productRequestDto.Price,
                Quantity = productRequestDto.Quantity
            };
            _mockProductService.Setup(x => x.UpdateProductAsync(productId, It.IsAny<Product>())).ReturnsAsync(updatedProduct);

            // Act
            var result = await _controller.Update(productId, productRequestDto);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }
        #endregion

        #region DeleteProduct
        [Fact]
        public async Task Delete_ReturnsOkResult_WithDeletedProduct()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var deletedProduct = new Product
            {
                Id = productId,
                Name = "Deleted Product",
                Price = 9.99m,
                Quantity = 10
            };
            _mockProductService.Setup(x => x.DeleteProductAsync(productId)).ReturnsAsync(deletedProduct);

            // Act
            var result = await _controller.Delete(productId);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }
        #endregion
    }
}
