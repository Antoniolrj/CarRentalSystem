namespace CarRentalSystem.Application.Errors;

/// <summary>
/// Error interno del servidor inesperado.
/// HTTP 500 - Internal Server Error
/// </summary>
public class InternalServerError : BaseError
{
    public string? ExceptionType { get; }
    public string? StackTrace { get; }

    public InternalServerError(string message, string? exceptionType = null, string? stackTrace = null, string? details = null)
        : base("INTERNAL_SERVER_ERROR", message, details)
    {
        ExceptionType = exceptionType;
        StackTrace = stackTrace;
    }

    public override int GetHttpStatusCode() => 500;

    /// <summary>
    /// Factory method para crear un error a partir de una excepción.
    /// </summary>
    public static InternalServerError FromException(Exception ex, string? message = null) =>
        new(
            message ?? "Ocurrió un error inesperado en el servidor.",
            ex.GetType().Name,
            ex.StackTrace,
            $"{ex.GetType().FullName}: {ex.Message}"
        );
}
