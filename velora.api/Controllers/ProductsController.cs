using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using velora.core.Data;
using velora.core.Repositories;
using velora.core.Specifications;

namespace velora.api.Controllers
{
    public class ProductsController : APIBaseController
    {
        private readonly IGenericRepository<Product> _productRepo;

        public ProductsController(IGenericRepository<Product> ProductRepo)
        {
            _productRepo = ProductRepo;
        }

        [HttpGet]
       public async Task<ActionResult<IEnumerable<Product>>> GetProducts() 
        {
            var Spec = new ProductWithBrandAndTypeSpecifications();
            var Products = await _productRepo.GetAllWithSpecAsync(Spec);
            return Ok(Products);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var Spec = new ProductWithBrandAndTypeSpecifications(id);
            var product = await _productRepo.GetByIdWithSpecAsync(Spec);
            return Ok(product);
        }
       
    }
}
