using Microsoft.OpenApi;

namespace UserManagementApi.Api.Extentions;
internal static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSwaggerWithAuth(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
    {
         c.SwaggerDoc("v1", new OpenApiInfo { Title = "User Management API", Version = "v1" });


        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        Description = "Enter token only"
        });

    
         c.AddSecurityRequirement(document => new OpenApiSecurityRequirement
        {
          [new OpenApiSecuritySchemeReference("Bearer", document)] = new List<string>()
        });
        });
        return services;
    }
}