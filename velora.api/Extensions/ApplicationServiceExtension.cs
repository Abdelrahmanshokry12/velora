using Microsoft.AspNetCore.Mvc;
using Store.Repository.Interfaces;
using Store.Services.HandlerResponses;
using velora.core.Entites;
using velora.repository.Interfaces;
using velora.repository.Repositories;
using velora.Repository.Repositories;
using velora.services.Services.AuthService;
using velora.services.Services.ContactsService;
using velora.services.Services.ContactsService.Dto;
using velora.services.Services.ProductService;
using velora.services.Services.ProductService.Dto;
using velora.services.Services.TokenService;

namespace velora.api.Extensions
{
    public static class ApplicationServiceExtension
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUnitWork, UnitWork>();
            services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
            services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));
            services.AddScoped<IContactsService, ContactsService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddAutoMapper(typeof(ContactsProfile));
            services.AddAutoMapper(typeof(ProductProfile).Assembly);
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IAuthService, AuthService>();


            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var errors = actionContext.ModelState
                        .Where(model => model.Value?.Errors.Count > 0)
                        .SelectMany(model => model.Value.Errors)
                        .Select(error => error.ErrorMessage)
                        .ToList();

                    var errorResponse = new ValidationErrorResponse
                    {
                        Errors = errors
                    };

                    return new BadRequestObjectResult(errorResponse);
                };
            }
            );
            return services;

        }
    }
}
