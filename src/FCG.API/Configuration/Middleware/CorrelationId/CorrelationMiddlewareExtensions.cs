namespace FCG.API.Configuration.Middleware.CorrelationId;

public static class CorrelationMiddlewareExtensions
{
    public static IApplicationBuilder UseCorrelationMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<CorrelationMiddleware>();
    }
}