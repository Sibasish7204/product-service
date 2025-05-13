using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProductCatalog.Core.Authorization;
using ProductCatalog.Core.Authorization.Implementation;
using ProductCatalog.Core.Authorization.Interface;
using ProductCatalog.Data.DbContext;
using ProductCatalog.Data.Implementations;
using ProductCatalog.Data.Implementations.ProductApi.Data;
using ProductCatalog.Data.Interfaces;
using ProductCatalog.Services.Interfaces;
using ProductCatalog.Services.Services;
using System.Text;

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
            services.AddScoped(typeof(IUserRepository), typeof(UserRepository));


            //Authentication:
            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
            var jwtSettings = configuration.GetSection("JwtSettings").Get<JwtSettings>();
            var key = Encoding.UTF8.GetBytes(jwtSettings.Key);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ClockSkew=TimeSpan.FromSeconds(10),

                        ValidIssuer = jwtSettings.Issuer,
                        ValidAudience = jwtSettings.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(key)
                    };
                });

            services.AddAuthorization();
            services.AddScoped<ITokenService, TokenService>();
            return services;
        }
    }
}
