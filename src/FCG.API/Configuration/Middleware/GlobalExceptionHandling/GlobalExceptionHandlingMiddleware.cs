using FCG.Domain.Common.Exceptions;
using FCG.Domain.Common.Response;
using FCG.Infrastructure.Log;

namespace FCG.API.Configuration.Middleware.GlobalExceptionHandling;

public class GlobalExceptionHandlingMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task Invoke(HttpContext context, BaseLogger logger)
    {
        try
        {
            await _next(context);
        }
        catch (EntityDoesNotExistException ex)
        {
            await CreateErrorResponse(context, logger, StatusCodes.Status404NotFound, ex, ex.Message);
        }
        catch (BadHttpRequestException ex)
        {
            await CreateErrorResponse(context, logger, StatusCodes.Status400BadRequest, ex, "Bad Request, please contact yor Administrator with the error id.");
        }
        catch (Exception ex)
        {
            await CreateErrorResponse(context, logger, StatusCodes.Status500InternalServerError, ex, "An unexpected fault happened, please contact yor Administrator with the error id.");
        }
    }

    private static async Task CreateErrorResponse(
        HttpContext context,
        BaseLogger logger,
        int statusCodes,
        Exception ex,
        string message)
    {
        var errorId = Guid.NewGuid();
        logger.LogError($"Error: {errorId}. {ex.StackTrace}");

        context.Response.Headers.Append("X-Error-ID", errorId.ToString());
        context.Response.StatusCode = statusCodes;
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsJsonAsync(new ErrorResponse
        {
            ErrorId = errorId.ToString(),
            Message = message,
            StatusCode = statusCodes
        });
    }
}

