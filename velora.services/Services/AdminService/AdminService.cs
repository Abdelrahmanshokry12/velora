using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Store.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using velora.core.Data;
using velora.core.Entities.IdentityEntities;
using velora.repository.Specifications.ProductSpecs;
using velora.services.Services.AdminService.Dto;
using velora.services.Services.AuthService.Dto;
using velora.services.Services.ProductService;
using velora.services.Services.ProductService.Dto;

namespace velora.services.Services.AdminService
{
    public class AdminService : IAdminService
    {
        private readonly IUnitWork _unitOfWork;
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        private readonly UserManager<Person> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminService(IUnitWork unitOfWork, UserManager<Person> userManager, RoleManager<IdentityRole> roleManager, IProductService productService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _roleManager = roleManager;
            _productService = productService;
            _mapper = mapper;
        }

        public async Task<ProductDto> CreateProductAsync(ProductDto productDto)
        {
            return await _productService.CreateProductAsync(productDto);
        }

        public async Task<ProductDto> UpdateProductAsync(int id, ProductDto productDto)
        {
            return await _productService.UpdateProductAsync(id, productDto);
        }

        public async Task<bool> UpdateProductStockAsync(int productId, int stockQuantity)
        {
            return await _productService.UpdateProductStockAsync(productId, stockQuantity);
        }
        public async Task<bool> DeleteProductAsync(int id)
        {
            return await _productService.DeleteProductAsync(id);
        }

        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            return await _productService.GetProductByIdAsync(id);
        }

        public async Task<List<ProductDto>> GetAllProductsAsync(ProductSpecification specParams)
        {
            return await _productService.GetAllProductsAsync(specParams);
        }

        //public async Task<IEnumerable<Order>> GetAllOrders()
        //    => await _unitOfWork.Repository<Order, int>().GetAllAsync();

        //public async Task<Order> GetOrderById(int orderId)
        //    => await _unitOfWork.Repository<Order, int>().GetByIdAsync(orderId);

        //public async Task UpdateOrderStatus(int orderId, string newStatus)
        //{
        //    var order = await _unitOfWork.Repository<Order, int>().GetByIdAsync(orderId);
        //    if (order != null)
        //    {
        //        order.Status = newStatus;
        //        _unitOfWork.Repository<Order, int>().Update(order);
        //        await _unitOfWork.CompleteAsync();
        //    }
        //}

        //public async Task<IEnumerable<UserManagementDto>> GetAllUsersAsync()
        //{
        //    var userRepository = _unitOfWork.PersonRepository(); 

        //    // Fetch all users from the repository
        //    var users = await userRepository.GetAllAsync();

        //    // Map the users to UserManagementDto
        //    var userDtos = _mapper.Map<IEnumerable<UserManagementDto>>(users);

        //    return userDtos;
        //}

        //public async Task<UserManagementDto> GetUserByIdAsync(string userId)
        //{
        //    var userRepository = _unitOfWork.PersonRepository();

        //    var user = await userRepository.GetByIdAsync(userId);

        //    if (user == null)
        //    {
        //        throw new Exception($"User with ID {userId} not found.");
        //    }
        //    var userDto = _mapper.Map<UserManagementDto>(user);

        //    return userDto;
        //}


        public async Task UpdateUserRoleAsync(string userId, string newRole)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                throw new Exception("User not found");

            var currentRoles = await _userManager.GetRolesAsync(user);

            var removeResult = await _userManager.RemoveFromRolesAsync(user, currentRoles);
            if (!removeResult.Succeeded)
                throw new Exception("Failed to remove old roles");

            var addResult = await _userManager.AddToRoleAsync(user, newRole);
            if (!addResult.Succeeded)
                throw new Exception("Failed to assign new role");
        }
    

       public async Task DeactivateUserAsync(string userId)
       {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
            throw new Exception("User not found");

        user.IsActive = false;

        var result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded)
            throw new Exception("Failed to deactivate user");
       }
    }   
}


