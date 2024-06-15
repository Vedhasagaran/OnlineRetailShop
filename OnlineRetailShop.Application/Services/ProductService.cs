using OnlineRetailShop.Application.Interfaces;
using OnlineRetailShop.Domain.DTO;
using OnlineRetailShop.Domain.Interfaces;

namespace OnlineRetailShop.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;        

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;         
        }

        

        public async Task<IEnumerable<ProductDto>> GetAll()
        {
            return await _productRepository.GetAll();
        }

        public async Task<ProductDto?> GetProductByIdAsync(Guid id)
        {
            return await _productRepository.GetByIdAsync(id);
        }

        public async Task<ProductDto> AddProductAsync(ProductRequestDto productRequestDto)
        {
            return await _productRepository.AddAsync(productRequestDto);
        }

        public async Task<ProductDto?> UpdateProductAsync(Guid id, ProductRequestDto productRequestDto)
        {
            return await _productRepository.UpdateAsync(id, productRequestDto);
        }

        public async Task DeleteProductAsync(Guid id)
        {
            await _productRepository.DeleteAsync(id);
        }
    }
}
