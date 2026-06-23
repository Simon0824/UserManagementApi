using UserManagementApi.Api;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApiDI();

var app = builder.Build();

app.UseHttpsRedirection();


app.Run();