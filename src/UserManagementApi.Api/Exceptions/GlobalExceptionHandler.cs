using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace UserManagementApi.Api.Exceptions;

public class GlobalExceptionHandler(IProblemDetailsService problemDetailsService, ILogger<GlobalExceptionHandler> logger) : IExceptionHandler 
{
    public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception ex, CancellationToken token)
    {
        logger.LogError("An unhandled error occured");

        context.Response.StatusCode = ex switch
        {
            ApplicationException => StatusCodes.Status400BadRequest,
            _ => StatusCodes.Status500InternalServerError
        };

        return await problemDetailsService.TryWriteAsync(new ProblemDetailsContext
        {
            HttpContext = context,
            Exception = ex,
            ProblemDetails = new ProblemDetails
            {
                Title = "Unknown error occured",
                Detail = ex.Message,
                Type = ex.GetType().Name
            }
        });
    }
}
