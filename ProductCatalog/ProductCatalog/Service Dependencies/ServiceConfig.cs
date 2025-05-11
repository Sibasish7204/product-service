using Microsoft.EntityFrameworkCore;
using ProductCatalog.Data.DbContext;
using ProductCatalog.Data.Implementations;
using ProductCatalog.Data.Interfaces;

namespace ProductCatalog.Service_Dependencies
{
    public static class ServiceConfig
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                                          options.UseSqlServer(configuration.GetConnectionString("SqlConnection")));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}
