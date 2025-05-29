namespace FCG.API.Configuration.Middleware.RequestLogging;

public static class RequestLoggingExtensions
{
    public static IApplicationBuilder UseRequestLogging(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<RequestLoggingMiddleware>();
    }
}

