using Moq;
using OnlineRetailShop.Application.Services;
using OnlineRetailShop.Domain.Interfaces;
using OnlineRetailShop.Domain.Models;
using OnlineRetailShop.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineRetailShop.Application.Tests.ServicesTests
{
    public class ProductServiceTests
    {
        private readonly Mock<IProductRepository> _mockProductRepository;
        private readonly ProductService _productService;

        public ProductServiceTests()
        {
            _mockProductRepository = new Mock<IProductRepository>();
            _productService = new ProductService(_mockProductRepository.Object);
        }

        [Fact]
        public async Task GetAll_ReturnsListOfProducts()
        {
            // Arrange
            var products = new List<Product>
        {
            new Product { Id = Guid.NewGuid(), Name = "Product 1", Price = 10.99m, Quantity = 5 },
            new Product { Id = Guid.NewGuid(), Name = "Product 2", Price = 19.99m, Quantity = 10 }
        };
            _mockProductRepository.Setup(x => x.GetAll()).ReturnsAsync(products);

            // Act
            var result = await _productService.GetAll();

            // Assert
            Assert.Equal(products, result);
        }

        [Fact]
        public async Task AddProductAsync_AddsProductToRepository()
        {
            // Arrange
            var product = new Product
            {
                Id = Guid.NewGuid(),
                Name = "Test Product",
                Price = 9.99m,
                Quantity = 10
            };
            _mockProductRepository.Setup(x => x.AddAsync(product)).ReturnsAsync(product);

            // Act
            var result = await _productService.AddProductAsync(product);

            // Assert
            Assert.Equal(product, result);
        }
        [Fact]
        public async Task GetProductByIdAsync_ReturnsProduct()
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
            _mockProductRepository.Setup(x => x.GetByIdAsync(productId)).ReturnsAsync(product);

            // Act
            var result = await _productService.GetProductByIdAsync(productId);

            // Assert
            Assert.Equal(product, result);
        }

        [Fact]
        public async Task UpdateProductAsync_ReturnsUpdatedProduct()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var updatedProduct = new Product
            {
                Id = productId,
                Name = "Updated Product",
                Price = 14.99m,
                Quantity = 20
            };
            _mockProductRepository.Setup(x => x.UpdateAsync(productId, updatedProduct)).ReturnsAsync(updatedProduct);

            // Act
            var result = await _productService.UpdateProductAsync(productId, updatedProduct);

            // Assert
            Assert.Equal(updatedProduct, result);
        }

        [Fact]
        public async Task DeleteProductAsync_ReturnsDeletedProduct()
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
            _mockProductRepository.Setup(x => x.DeleteAsync(productId)).ReturnsAsync(deletedProduct);

            // Act
            var result = await _productService.DeleteProductAsync(productId);

            // Assert
            Assert.Equal(deletedProduct, result);
        }
    }
}
