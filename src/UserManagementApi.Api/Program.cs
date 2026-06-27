using System.Runtime.InteropServices;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using Microsoft.VisualBasic;
using UserManagementApi.Api;
using UserManagementApi.Api.Exceptions;
using UserManagementApi.Api.Extentions;
using UserManagementApi.Domain.Constants;
using UserManagementApi.Domain.Entities;
using UserManagementApi.Infrastructure.Data;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);
builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddControllers();

builder.Services.AddSwaggerWithAuth();

builder.Services.AddApiDI(builder.Configuration);

builder.Services.AddProblemDetails(config =>
{
   config.CustomizeProblemDetails = context =>
   {
      context.ProblemDetails.Extensions.Add("reqId", context.HttpContext.TraceIdentifier);   
   };
});

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.WebHost.UseUrls("http://0.0.0.0:8080");

var app = builder.Build();

if(app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    using var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.Migrate();

    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<UserEntity>>();

    if(!await roleManager.RoleExistsAsync(Roles.Admin))
    {
        await roleManager.CreateAsync(new IdentityRole(Roles.Admin));
    }
    if(!await roleManager.RoleExistsAsync(Roles.Member))
    {
        await roleManager.CreateAsync(new IdentityRole(Roles.Member));
    }

    var adminEmail = "admin@user.com";
    if(await userManager.FindByEmailAsync(adminEmail) is null)
    {
        UserEntity admin = new()
        {
          FullName = "Admin",
          Email = adminEmail,  
          UserName = adminEmail
        };
        var result = await userManager.CreateAsync(admin, "Admin123!");
        if(result.Succeeded)
        {
            await userManager.UpdateAsync(admin);
            await userManager.AddToRoleAsync(admin, Roles.Admin);
        }
        else
        {
               Console.WriteLine(string.Join(", ", result.Errors.Select(e => e.Description)));
        }
    }
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();