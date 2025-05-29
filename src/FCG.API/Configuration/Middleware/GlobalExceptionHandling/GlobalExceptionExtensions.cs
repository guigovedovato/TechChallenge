namespace FCG.API.Configuration.Middleware.GlobalExceptionHandling;

public static class GlobalExceptionExtensions
{
    public static IApplicationBuilder UseGlobalExceptionHandling(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<GlobalExceptionHandlingMiddleware>();
    }
}

