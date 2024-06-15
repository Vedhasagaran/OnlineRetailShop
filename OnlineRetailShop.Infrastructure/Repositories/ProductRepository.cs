using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineRetailShop.Domain.DTO;
using OnlineRetailShop.Domain.Interfaces;
using OnlineRetailShop.Domain.Models;
using OnlineRetailShop.Infrastructure.Data;
using OnlineRetailShop.Utilities.Exceptions;

namespace OnlineRetailShop.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly OnlineRetailShopDbContext _onlineRetailShopDbContext;
        private readonly IMapper _mapper;

        public ProductRepository(OnlineRetailShopDbContext onlineRetailShopDbContext,IMapper mapper)
        {
            _onlineRetailShopDbContext = onlineRetailShopDbContext;
            _mapper = mapper;
        }   

        public async Task<IEnumerable<ProductDto>> GetAll()
        {
            var product = await _onlineRetailShopDbContext.Products.ToListAsync();

            return _mapper.Map<List<ProductDto>>(product.ToList());

        }

        public async Task<ProductDto?> GetByIdAsync(Guid productId)
        {
            var product = await _onlineRetailShopDbContext.Products.FirstOrDefaultAsync(n => n.Id == productId);
            if (product == null)
            {
                throw new NotFoundException("Product not found");
            }
            return _mapper.Map<ProductDto>(product);
        }

        public async Task<ProductDto> AddAsync(ProductRequestDto productRequestDto)
        {
            var product = _mapper.Map<Product>(productRequestDto);
            await _onlineRetailShopDbContext.Products.AddAsync(product);
            await _onlineRetailShopDbContext.SaveChangesAsync();
            var productDto = _mapper.Map<ProductDto>(product);
            return productDto;
        }

        public async Task<ProductDto?> UpdateAsync(Guid id, ProductRequestDto productRequestDto)
        {
            var productDetail = await _onlineRetailShopDbContext.Products.FirstOrDefaultAsync(n => n.Id == id);
            if (productDetail == null)
                return null;

            productDetail.Name = productRequestDto.Name;
            productDetail.Price = productRequestDto.Price;
            productDetail.Quantity = productRequestDto.Quantity;
           
            await _onlineRetailShopDbContext.SaveChangesAsync();


            return _mapper.Map<ProductDto>(productDetail);                       
        }

        public async Task DeleteAsync(Guid id)
        {
            var productDetail = await _onlineRetailShopDbContext.Products.FirstOrDefaultAsync(n => n.Id == id);
            if (productDetail == null)
            {
                throw new NotFoundException("Product not found");
            }               
            _onlineRetailShopDbContext.Products.Remove(productDetail);
            await _onlineRetailShopDbContext.SaveChangesAsync();            
        }
    }
}
