using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using PashaBank.Repository.Implementations;
using PashaBank.Services.Implementation.Auth;
using SABA.Core.Abstractions;
using SABA.Persistance.Implementations;
using SABA.Services.Abstractiuons.Auth;
using SABA.Services.Abstractiuons.Email;
using SABA.Services.Abstractiuons.ProductSaleService;
using SABA.Services.Abstractiuons.Recomentations;
using SABA.Services.Abstractiuons.User;
using SABA.Services.Implementations.Auth;
using SABA.Services.Implementations.Email;
using SABA.Services.Implementations.ProductSaleService;
using SABA.Services.Implementations.Recomendation;
using SABA.Services.Implementations.User;
using System.Text;
using System.Text.Json.Serialization;

namespace SABA.web.ExtensionMethods
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration.GetSection("JWTConfiguration:Secret").Value))
                };
            });
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IRecommendationRepository, RecommendationRepository>();
            services.AddScoped<ISaleRepository, SaleRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IProductSaleService, ProductSaleService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IRecomendationServices, RecomendationServices>();
            services.AddScoped<IEmailSendService, EmailSendService>();
            //services.AddScoped<IBonusService, BonusService>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
                });

            return services;
        }
    }
}
