using OnlineRetailShop.Domain.DTO;

namespace OnlineRetailShop.Domain.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<ProductDto>> GetAll();
        Task<ProductDto?> GetByIdAsync(Guid productId);
        Task<ProductDto> AddAsync(ProductRequestDto productRequestDto);
        Task<ProductDto?> UpdateAsync(Guid id, ProductRequestDto productRequestDto);
        Task DeleteAsync(Guid id);
    }
}
