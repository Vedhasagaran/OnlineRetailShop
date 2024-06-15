using Microsoft.EntityFrameworkCore;
using OnlineRetailShop.Domain.Interfaces;
using OnlineRetailShop.Domain.Models;
using OnlineRetailShop.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineRetailShop.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly OnlineRetailShopDbContext _onlineRetailShopDbContext;

        public ProductRepository(OnlineRetailShopDbContext onlineRetailShopDbContext)
        {
            _onlineRetailShopDbContext = onlineRetailShopDbContext;
        }
        public async Task<Product> AddAsync(Product product)
        {
            await _onlineRetailShopDbContext.Products.AddAsync(product);
            await _onlineRetailShopDbContext.SaveChangesAsync();
            return product;
        }

        public async Task<Product?> DeleteAsync(Guid id)
        {
            var productDetail = await _onlineRetailShopDbContext.Products.FirstOrDefaultAsync(n => n.Id == id);
            if (productDetail == null)
                return null;

            _onlineRetailShopDbContext.Products.Remove(productDetail);
            await _onlineRetailShopDbContext.SaveChangesAsync();
            return productDetail;
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _onlineRetailShopDbContext.Products.ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(Guid productId)
        {
            return await _onlineRetailShopDbContext.Products.FirstOrDefaultAsync(n => n.Id == productId);
        }

        public async Task<Product?> UpdateAsync(Guid id, Product product)
        {
            var productDetail = await _onlineRetailShopDbContext.Products.FirstOrDefaultAsync(n => n.Id == id);
            if (productDetail == null)
                return null;

            productDetail.Name = product.Name;
            productDetail.Price = product.Price;
            productDetail.Quantity = product.Quantity;
           
            await _onlineRetailShopDbContext.SaveChangesAsync();
            return productDetail;                       
        }
    }
}
