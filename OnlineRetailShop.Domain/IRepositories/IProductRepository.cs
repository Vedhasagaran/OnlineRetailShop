using OnlineRetailShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineRetailShop.Domain.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAll();
        Task<Product?> GetByIdAsync(Guid productId);
        Task<Product> AddAsync(Product product);
        Task<Product?> UpdateAsync(Guid id,Product product);
        Task<Product?> DeleteAsync(Guid id);
    }
}
