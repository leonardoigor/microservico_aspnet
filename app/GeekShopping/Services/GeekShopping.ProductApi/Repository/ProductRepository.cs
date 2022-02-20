using AutoMapper;
using GeekShopping.ProductApi.Data.ValueObjects;
using GeekShopping.ProductApi.Model;
using GeekShopping.ProductApi.Model.Context;
using Microsoft.EntityFrameworkCore;

namespace GeekShopping.ProductApi.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly MysqlContext _context;
        private IMapper _mapper;
        public ProductRepository(MysqlContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ProductVO> Create(ProductVO product)
        {
            var productFromDb = _mapper.Map<Product>(product);
            _context.Products.Add(productFromDb);
            await _context.SaveChangesAsync();
            return _mapper.Map<ProductVO>(productFromDb);
        }

        public async Task<bool> Delete(long id)
        {
            try
            {

                var existProduct = await ProductExists(id);
                if (existProduct != null)
                {
                    _context.Products.Remove(existProduct);
                    _context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (System.Exception)
            {
                return false;
            }
        }

        public async Task<List<ProductVO>> GetAll()
        {
            var products = await _context.Products.AsNoTracking().ToListAsync();
            var mapper = _mapper.Map<List<ProductVO>>(products);
            return mapper;
        }

        public async Task<ProductVO> GetById(long id)
        {
            var product = await _context.Products.FindAsync(id);
            var mapper = _mapper.Map<ProductVO>(product);
            return mapper;
        }

        public async Task<ProductVO> Update(ProductVO product)
        {
            var existProduct = await ProductExists(product.Id);

            if (existProduct != null)
            {
                var mapProduct= _mapper.Map<Product>(product);
                _context.Products.Update(mapProduct);
                await _context.SaveChangesAsync();
                return _mapper.Map<ProductVO>(mapProduct);
            }
            return null;
        }

        private async Task<Product?> ProductExists(long id)
        {
            var product = await _context.Products.AsNoTracking().Where(e => e.Id == id).FirstOrDefaultAsync();


            return product;
        }
    }
}