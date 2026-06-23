using UserManagementApi.Application;
using UserManagementApi.Infrastructure;

namespace UserManagementApi.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddApiDI(this IServiceCollection services)
    {
        services.AddApplicationDI()
                .AddInfrastructureDI();
        return services;
    }
}
