using CarRentalSystem.Application.Errors;

namespace CarRentalSystem.Presentation.Middleware;

/// <summary>
/// Middleware que captura las excepciones no manejadas y las convierte a respuestas HTTP.
/// </summary>
public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Excepción no manejada: {ExceptionType}", ex.GetType().Name);
            await HandleExceptionAsync(context, ex);
        }
    }

    /// <summary>
    /// Mapea excepciones a BaseError y devuelve respuesta HTTP.
    /// </summary>
    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        BaseError error = exception switch
        {
            // Excepciones de argumentos (validación)
            ArgumentException argEx => BadRequestError.Validation(argEx.Message),

            // Excepción genérica
            _ => InternalServerError.FromException(exception)
        };

        context.Response.StatusCode = error.GetHttpStatusCode();

        var response = new
        {
            error = new
            {
                code = error.Code,
                message = error.Message,
                details = error.Details
            }
        };

        return context.Response.WriteAsJsonAsync(response);
    }
}
