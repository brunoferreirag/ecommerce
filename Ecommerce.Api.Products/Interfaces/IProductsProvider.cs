using Ecommerce.Api.Products.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Api.Products.Interfaces
{
    public interface IProductsProvider
    {
        Task<(bool IsSuccess, IEnumerable<Product> Products, String ErrorMessage)> GetProductAsync();
        Task<(bool IsSuccess, Product Product, String ErrorMessage)> GetProductByIdAsync(int id);
    }
}
