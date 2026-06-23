using Microsoft.Extensions.DependencyInjection;

namespace UserManagementApi.Domain;

public static class DependencyInjection
{
    public static IServiceCollection AddDomainDI(this IServiceCollection services)
    {
        return services;
    }
}