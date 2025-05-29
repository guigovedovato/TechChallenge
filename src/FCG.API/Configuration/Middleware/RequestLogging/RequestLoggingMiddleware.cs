using FCG.Infrastructure.Log;

namespace FCG.API.Configuration.Middleware.RequestLogging;

public class RequestLoggingMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task Invoke(HttpContext context, BaseLogger logger)
    {
        logger.LogInformation($"Request: {context.Request.Method} {context.Request.Path}");
        await _next(context);
        logger.LogInformation($"Response: {context.Response.StatusCode}");
    }
}

