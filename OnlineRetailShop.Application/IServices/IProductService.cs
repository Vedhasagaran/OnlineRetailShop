using OnlineRetailShop.Domain.DTO;

namespace OnlineRetailShop.Application.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAll();
        Task<ProductDto?> GetProductByIdAsync(Guid id);
        Task<ProductDto> AddProductAsync(ProductRequestDto productRequestDto);
        Task<ProductDto?> UpdateProductAsync(Guid id, ProductRequestDto productRequestDto);
        Task DeleteProductAsync(Guid id);
    }
}
