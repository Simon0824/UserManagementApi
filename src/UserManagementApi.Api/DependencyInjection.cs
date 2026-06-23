using UserManagementApi.Application;
using UserManagementApi.Infrastructure;

namespace UserManagementApi.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddApiDI(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddApplicationDI()
                .AddInfrastructureDI(configuration);
        return services;
    }
}
