using DataAccessLayer.Context;
using DataAccessLayer.Repositories;
using DataAccessLayer.RepositoryContracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
 

namespace DataAccessLayer; 
 
public static class DependencyInjection
{
    public static IServiceCollection AddDataAccessLayer(this IServiceCollection services,IConfiguration configuration)
    {
        // Register your data access layer services here
        // For example:
        // services.AddScoped<IYourRepository, YourRepository>();

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseMySQL(configuration.GetConnectionString("DefaultConnection")!));
        services.AddScoped<IProductRepository, ProductRepository>();
        return services;
    }
}
 
