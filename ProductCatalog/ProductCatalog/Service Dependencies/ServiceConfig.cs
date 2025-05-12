using Microsoft.EntityFrameworkCore;
using ProductCatalog.Data.DbContext;
using ProductCatalog.Data.Implementations;
using ProductCatalog.Data.Implementations.ProductApi.Data;
using ProductCatalog.Data.Interfaces;
using ProductCatalog.Services.Interfaces;
using ProductCatalog.Services.Services;

namespace ProductCatalog.Service_Dependencies
{
    public static class ServiceConfig
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                                          options.UseSqlServer(configuration.GetConnectionString("SqlConnection")));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IShoppingCartService, ShoppingCartService>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IUserService, UserService>();    
            return services;
        }
    }
}
