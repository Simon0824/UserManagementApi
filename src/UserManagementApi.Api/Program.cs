using FluentValidation;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using Microsoft.VisualBasic;
using UserManagementApi.Api;
using UserManagementApi.Api.Exceptions;
using UserManagementApi.Api.Extentions;
using UserManagementApi.Infrastructure.Data;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

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

var app = builder.Build();

if(app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    using var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.Migrate();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();