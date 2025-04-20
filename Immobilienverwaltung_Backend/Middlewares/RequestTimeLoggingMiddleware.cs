using System.Diagnostics;

namespace Immobilienverwaltung_Backend.Middlewares;

public class RequestTimeLoggingMiddleware(ILogger<RequestTimeLoggingMiddleware> logger) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var sw = Stopwatch.StartNew();
        await next.Invoke(context);
        sw.Stop();

        if (sw.ElapsedMilliseconds / 1000 > 4)
        {
            logger.LogInformation("Request [{Verb}] at [{Path}] took [{Time}] ms", 
                context.Request.Method, 
                context.Request.Path, 
                sw.ElapsedMilliseconds);
        }
    }
}
  
