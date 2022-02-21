using GeekShopping.Web.Models;
using GeekShopping.Web.Services.IServices;
using GeekShopping.Web.Utils;

namespace GeekShopping.Web.Services
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;
        public const string BasePath = "api/vr/product";

        public ProductService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<ProductModel>> GetAllProducts()
        {
            var response = await _httpClient.GetAsync(BasePath);

            var result = await response.ReadContenteAs<IEnumerable<ProductModel>>();

            if (result == null)
            {
                throw new ApplicationException("Error getting products");
            }
            return result;
        }

        public async Task<ProductModel> GetProductById(long id)
        {
            var response = await _httpClient.GetAsync($"{BasePath}/{id}");

            var result = await response.ReadContenteAs<ProductModel>();
            if (result == null)
            {
                throw new ApplicationException("Error getting product");
            }
            return result;
        }
        public async Task<ProductModel> CreateProduct(ProductModel product)
        {
            var response = await _httpClient.PostAsJsonAsync(BasePath, product);

            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException("Error creating product");
            }
            var result = await response.ReadContenteAs<ProductModel>();
            if (result == null)
            {
                throw new ApplicationException("Error creating product");
            }
            return result;
        }

        public async Task<bool> deleteProduct(long id)
        {
            var response = await _httpClient.PostAsync($"{BasePath}/{id}/delete", null);
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException("Error deleting product");
            }
            var result = await response.ReadContenteAs<bool>();

            return result;
        }


        public async Task<ProductModel> UpdateProduct(ProductModel product)
        {
           var response = await _httpClient.PostAsJsonAsync(BasePath, product);

            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException("Error updating product");
            }
            var result =await response.ReadContenteAs<ProductModel>();
            if (result == null)
            {
                throw new ApplicationException("Error updating product");
            }
            return result;
        }
    }
}