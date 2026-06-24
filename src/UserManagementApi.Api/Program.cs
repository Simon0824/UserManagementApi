using FluentValidation;
using UserManagementApi.Api;
using UserManagementApi.Api.Exceptions;
using UserManagementApi.Api.Extentions;
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
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();