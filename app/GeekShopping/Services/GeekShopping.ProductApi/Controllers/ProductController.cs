using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeekShopping.ProductApi.Data.ValueObjects;
using GeekShopping.ProductApi.Repository;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.ProductApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }


        [HttpGet]
        public async Task<ActionResult<List<ProductVO>>> GetAllProducts()
        {
            var products = await _productRepository.GetAll();

            return Ok(products);
        }

        [HttpPost]
        public async Task<ActionResult<ProductVO>> CreateProduct(ProductVO product)
        {
            var newProduct = await _productRepository.Create(product);
            return CreatedAtAction(nameof(GetAllProducts), newProduct);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductVO>> GetProduct(long id)
        {
            var product = await _productRepository.GetById(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
        [HttpPost("/update")]
        public async Task<ActionResult<ProductVO>> UpdateProduct(ProductVO product)
        {
            var updatedProduct = await _productRepository.Update(product);
            if (updatedProduct == null)
            {
                return NotFound();
            }
            return Ok(updatedProduct);
        }
        [HttpPost("{id}/delete")]
        public async Task<ActionResult<ProductVO>> DeleteProduct(long id)
        {
            var deletedProduct = await _productRepository.Delete(id);
            if (deletedProduct)
            {
                return Ok(deletedProduct);
            }
            return NotFound();
        }
    }
}