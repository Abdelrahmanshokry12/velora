using AutoMapper;
using Store.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using velora.core.Data;
using velora.repository.Specifications.ProductSpecs;
using velora.services.Services.ProductService.Dto;

namespace velora.services.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly IUnitWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<ProductDto>> GetAllProductsAsync(ProductSpecification specParams)
        {
            var repo = _unitOfWork.Repository<Product, int>();
            var spec = new ProductWithSpecification(specParams);
            var products = await repo.GetAllWithSpecAsync(spec);
            return _mapper.Map<List<ProductDto>>(products);
        }

        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var repo = _unitOfWork.Repository<Product, int>();
            var spec = new ProductWithSpecification(id);
            var product = await repo.GetByIdWithSpecAsync(spec);
            return _mapper.Map<ProductDto>(product);
        }

        public async Task<int> GetTotalCountAsync(ProductSpecification specParams)
        {
            var repo = _unitOfWork.Repository<Product, int>();
            var countSpec = new ProductWithCountSpecification(specParams);
            return await repo.CountAsync(countSpec);
        }

        //public async Task CreateProductAsync(ProductDto dto)
        //{
        //    var repo = _unitOfWork.Repository<Product, int>();

        //    var product = _mapper.Map<Product>(dto);
        //    product.CreatedAt = DateTime.UtcNow;

        //    await repo.AddAsync(product);
        //    await _unitOfWork.CompleteAsync();
        //}
    }
}
