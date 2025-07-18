using Microsoft.Extensions.DependencyInjection;
 using BusinessLogicLayer.Mappers;
using BusinessLogicLayer.ServiceContracts;
using BusinessLogicLayer.Services;
using FluentValidation;
using BusinessLogicLayer.Validators;

namespace BusinessLogicLayer; 
 
public static class DependencyInjection
{
    public static IServiceCollection AddBusinessAccessLayer(this IServiceCollection services)
    {
        // Register your data access layer services here
        // For example:
        // services.AddScoped<IYourRepository, YourRepository>();
        services.AddAutoMapper(typeof(ProductAddRequestToProductMappingProfile).Assembly);
        services.AddValidatorsFromAssemblyContaining<ProductAddRequestValidator>();
        services.AddScoped<IProductService, ProductService>();
        return services;
    }
}
 
