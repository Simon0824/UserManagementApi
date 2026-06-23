using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserManagementApi.Domain.Interfaces;
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
        services.AddScoped<IUserRepository, UserRepository>();
        return services;
    }
}
