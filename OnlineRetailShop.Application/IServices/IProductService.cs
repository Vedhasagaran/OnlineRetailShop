using OnlineRetailShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineRetailShop.Application.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAll();

        Task<Product> AddProductAsync(Product product);

        Task<Product?> GetProductByIdAsync(Guid id);

        Task<Product?> UpdateProductAsync(Guid id, Product product);

        Task<Product?> DeleteProductAsync(Guid id);


    }
}
