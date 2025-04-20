using BE.Domain.Exceptions;

namespace Immobilienverwaltung_Backend.Middlewares
{
    public class ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger) : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (NotFoundException notFound)
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync(notFound.Message);

                logger.LogWarning(notFound.Message);
            }
            catch (InvalidOperationException invalidOp)
            {
                context.Response.StatusCode = 409;
                await context.Response.WriteAsync(invalidOp.Message);

                logger.LogWarning(invalidOp.Message);
            }
            catch (Exception ex) 
            {
                logger.LogError(ex, ex.Message);

                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("Something went wrong");
            }
        }
    }
}
