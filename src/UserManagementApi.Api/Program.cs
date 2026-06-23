using UserManagementApi.Api;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApiDI(builder.Configuration);

var app = builder.Build();

app.UseHttpsRedirection();


app.Run();