using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeekShopping.Web.Models;

namespace GeekShopping.Web.Services.IServices
{
    public interface IProductService
    {
        Task<IEnumerable<ProductModel>> GetAllProducts();
        Task<ProductModel> GetProductById(long id);
        Task<bool> deleteProduct(long id);
        Task<ProductModel> UpdateProduct(ProductModel product);
        Task<ProductModel> CreateProduct(ProductModel product);

    }
}