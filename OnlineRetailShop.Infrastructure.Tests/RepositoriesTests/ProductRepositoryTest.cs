using AutoMapper;
using EntityFrameworkCoreMock;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Moq;
using OnlineRetailShop.Domain.DTO;
using OnlineRetailShop.Domain.Mappings;
using OnlineRetailShop.Domain.Models;
using OnlineRetailShop.Infrastructure.Data;
using OnlineRetailShop.Infrastructure.Repositories;
using OnlineRetailShop.Utilities.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineRetailShop.Infrastructure.Tests.RepositoriesTests
{
    public class ProductRepositoryTests
    {
        private readonly Mock<IMapper> _mockMapper;
        private readonly ProductRepository _repository;
        private readonly DbContextMock<OnlineRetailShopDbContext> _mockDbContext;

        public ProductRepositoryTests()
        {
            var productsInitialData = new List<Product>
            {
                new Product { Id = Guid.NewGuid(), Name = "Product 1", Price = 100, Quantity = 10 },
                new Product { Id = Guid.NewGuid(), Name = "Product 2", Price = 200, Quantity = 20 }
            };

            _mockDbContext = new DbContextMock<OnlineRetailShopDbContext>(new DbContextOptionsBuilder<OnlineRetailShopDbContext>().Options);
            _mockDbContext.CreateDbSetMock(context => context.Products, productsInitialData);

            _mockMapper = new Mock<IMapper>();
            _repository = new ProductRepository(_mockDbContext.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task GetAll_ShouldReturnAllProducts()
        {
            // Arrange
            var productDtos = new List<ProductDto>
            {
                new ProductDto { Id = Guid.NewGuid(), Name = "Product 1", Price = 100, Quantity = 10 },
                new ProductDto { Id = Guid.NewGuid(), Name = "Product 2", Price = 200, Quantity = 20 }
            };
            _mockMapper.Setup(m => m.Map<List<ProductDto>>(It.IsAny<List<Product>>())).Returns(productDtos);

            // Act
            var result = await _repository.GetAll();

            // Assert
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnProduct_WhenProductExists()
        {
            // Arrange
            var productId = _mockDbContext.Object.Products.First().Id;
            var productDto = new ProductDto { Id = productId, Name = "Product 1", Price = 100, Quantity = 10 };
            _mockMapper.Setup(m => m.Map<ProductDto>(It.IsAny<Product>())).Returns(productDto);

            // Act
            var result = await _repository.GetByIdAsync(productId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(productId, result.Id);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldThrowNotFoundException_WhenProductDoesNotExist()
        {
            // Arrange

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => _repository.GetByIdAsync(Guid.NewGuid()));
        }

        [Fact]
        public async Task AddAsync_ShouldAddProduct()
        {
            // Arrange
            var productRequestDto = new ProductRequestDto { Name = "Product 3", Price = 300, Quantity = 30 };
            var product = new Product { Id = Guid.NewGuid(), Name = "Product 3", Price = 300, Quantity = 30 };

            _mockMapper.Setup(m => m.Map<Product>(productRequestDto)).Returns(product);
            _mockMapper.Setup(m => m.Map<ProductDto>(product)).Returns(new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Quantity = product.Quantity
            });

            // Act
            var result = await _repository.AddAsync(productRequestDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(product.Name, result.Name);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateProduct()
        {
            // Arrange
            var productId = _mockDbContext.Object.Products.First().Id;
            var productRequestDto = new ProductRequestDto { Name = "Updated Product", Price = 150, Quantity = 5 };
            var productDto = new ProductDto { Id = productId, Name = "Updated Product", Price = 150, Quantity = 5 };

            _mockMapper.Setup(m => m.Map<ProductDto>(It.IsAny<Product>())).Returns(productDto);

            // Act
            var result = await _repository.UpdateAsync(productId, productRequestDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(productRequestDto.Name, result.Name);
        }

        [Fact]
        public async Task UpdateAsync_ShouldReturnNull_WhenProductDoesNotExist()
        {
            // Arrange

            // Act
            var result = await _repository.UpdateAsync(Guid.NewGuid(), new ProductRequestDto());

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task DeleteAsync_ShouldDeleteProduct()
        {
            // Arrange
            var productId = _mockDbContext.Object.Products.First().Id;

            // Act
            await _repository.DeleteAsync(productId);

            // Assert
            Assert.Null(_mockDbContext.Object.Products.FirstOrDefault(p => p.Id == productId));
        }

        [Fact]
        public async Task DeleteAsync_ShouldThrowNotFoundException_WhenProductDoesNotExist()
        {
            // Arrange

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => _repository.DeleteAsync(Guid.NewGuid()));
        }
    }
}

