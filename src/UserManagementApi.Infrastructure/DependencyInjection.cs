using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using UserManagementApi.Domain.Entities;
using UserManagementApi.Domain.Interfaces;
using UserManagementApi.Infrastructure.Auth;
using UserManagementApi.Infrastructure.Data;
using UserManagementApi.Infrastructure.Repositories;
namespace UserManagementApi.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureDI(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
                    options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
        });

        services.AddMemoryCache();
        services.AddStackExchangeRedisCache(redisOptions =>
        {
            string connstring = configuration.GetConnectionString("Redis")!;

            redisOptions.Configuration = connstring;
        });

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ITokenProvider, TokenProvider>();
        services.Decorate<IUserRepository, CachedUserRepository>();

        services.AddIdentity<UserEntity, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

        services.AddAuthorization();
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters.ValidIssuer = configuration["Jwt:Issuer"];
            options.TokenValidationParameters.ValidAudience = configuration["Jwt:Audience"];
            options.TokenValidationParameters.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Secret"]!));
        });

        return services;
    }
}
