using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using velora.repository.Specifications.ProductSpecs;
using velora.services.Services.ProductService;
using velora.services.Services.ProductService.Dto;

namespace velora.api.Controllers
{
    public class ProductsController : APIBaseController
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        // 🔎 Get All Products with Filtering, Paging
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] ProductSpecification filters)
        {
            var products = await _productService.GetAllProductsAsync(filters);
            var count = await _productService.GetTotalCountAsync(filters);

            var response = new
            {
                pageIndex = filters.PageIndex,
                pageSize = filters.PageSize,
                count ,
                data = products
            };

            return Ok(response);
        }

        // 🔍 Get Single Product by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null) return NotFound();

            return Ok(product);
        }

        //// ✏️ Add New Product (optional - Admin panel)
        //[HttpPost]
        //public async Task<IActionResult> CreateProduct([FromBody] ProductDto dto)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    await _productService.CreateProductAsync(dto);
        //    return StatusCode(201);
        //}
    }
}
