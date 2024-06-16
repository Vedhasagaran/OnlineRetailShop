using Moq;
using OnlineRetailShop.Application.Services;
using OnlineRetailShop.Domain.DTO;
using OnlineRetailShop.Domain.Interfaces;
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
        public async Task GetAll_ShouldReturnAllProducts()
        {
            // Arrange
            var products = new List<ProductDto>
            {
                new ProductDto { Id = Guid.NewGuid(), Name = "Product 1", Price = 10, Quantity = 1 },
                new ProductDto { Id = Guid.NewGuid(), Name = "Product 2", Price = 20, Quantity = 2 }
            };

            _mockProductRepository.Setup(repo => repo.GetAll()).ReturnsAsync(products);

            // Act
            var result = await _productService.GetAll();

            // Assert
            Assert.Equal(products.Count, result.Count());
            Assert.Equal(products, result);
        }

        [Fact]
        public async Task GetProductByIdAsync_ShouldReturnProduct_WhenProductExists()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var product = new ProductDto { Id = productId, Name = "Product 1", Price = 10, Quantity = 1 };

            _mockProductRepository.Setup(repo => repo.GetByIdAsync(productId)).ReturnsAsync(product);

            // Act
            var result = await _productService.GetProductByIdAsync(productId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(productId, result.Id);
        }

        [Fact]
        public async Task GetProductByIdAsync_ShouldReturnNull_WhenProductDoesNotExist()
        {
            // Arrange
            _mockProductRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((ProductDto)null);

            // Act
            var result = await _productService.GetProductByIdAsync(Guid.NewGuid());

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task AddProductAsync_ShouldAddProduct()
        {
            // Arrange
            var productRequestDto = new ProductRequestDto { Name = "New Product", Price = 30, Quantity = 5 };
            var addedProduct = new ProductDto { Id = Guid.NewGuid(), Name = productRequestDto.Name, Price = productRequestDto.Price, Quantity = productRequestDto.Quantity };

            _mockProductRepository.Setup(repo => repo.AddAsync(productRequestDto)).ReturnsAsync(addedProduct);

            // Act
            var result = await _productService.AddProductAsync(productRequestDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(addedProduct.Id, result.Id);
            Assert.Equal(addedProduct.Name, result.Name);
        }

        [Fact]
        public async Task UpdateProductAsync_ShouldUpdateProduct()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var productRequestDto = new ProductRequestDto { Name = "Updated Product", Price = 40, Quantity = 10 };
            var updatedProduct = new ProductDto { Id = productId, Name = productRequestDto.Name, Price = productRequestDto.Price, Quantity = productRequestDto.Quantity };

            _mockProductRepository.Setup(repo => repo.UpdateAsync(productId, productRequestDto)).ReturnsAsync(updatedProduct);

            // Act
            var result = await _productService.UpdateProductAsync(productId, productRequestDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(updatedProduct.Id, result.Id);
            Assert.Equal(updatedProduct.Name, result.Name);
        }

        [Fact]
        public async Task UpdateProductAsync_ShouldReturnNull_WhenProductDoesNotExist()
        {
            // Arrange
            _mockProductRepository.Setup(repo => repo.UpdateAsync(It.IsAny<Guid>(), It.IsAny<ProductRequestDto>())).ReturnsAsync((ProductDto)null);

            // Act
            var result = await _productService.UpdateProductAsync(Guid.NewGuid(), new ProductRequestDto());

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task DeleteProductAsync_ShouldCallDeleteOnRepository()
        {
            // Arrange
            var productId = Guid.NewGuid();

            // Act
            await _productService.DeleteProductAsync(productId);

            // Assert
            _mockProductRepository.Verify(repo => repo.DeleteAsync(productId), Times.Once);
        }
    }
}
