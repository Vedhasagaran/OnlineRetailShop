using OnlineRetailShop.Application.Interfaces;
using OnlineRetailShop.Domain.Interfaces;
using OnlineRetailShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineRetailShop.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }        

        public async Task<Product> AddProductAsync(Product product)
        {
            return await _productRepository.AddAsync(product);
        }

        public async Task<Product?> DeleteProductAsync(Guid id)
        {
            return await _productRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _productRepository.GetAll();
        }

        public async Task<Product?> GetProductByIdAsync(Guid id)
        {
            return await _productRepository.GetByIdAsync(id);
        }

        public async Task<Product?> UpdateProductAsync(Guid id, Product product)
        {
            return await _productRepository.UpdateAsync(id,product);
        }
    }
}
