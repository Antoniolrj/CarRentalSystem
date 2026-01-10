namespace CarRentalSystem.Application.Errors;

/// <summary>
/// Clase base para todos los errores de la aplicación.
/// </summary>
public abstract class BaseError
{
    /// <summary>
    /// Código de error único para identificar el tipo de error.
    /// </summary>
    public string Code { get; protected set; }

    /// <summary>
    /// Mensaje de error descriptivo para el cliente.
    /// </summary>
    public string Message { get; protected set; }

    /// <summary>
    /// Detalles adicionales del error (opcional).
    /// Útil para debugging y logs.
    /// </summary>
    public string? Details { get; protected set; }

    protected BaseError(string code, string message, string? details = null)
    {
        Code = code;
        Message = message;
        Details = details;
    }

    /// <summary>
    /// Método para obtener el estado HTTP recomendado para este error.
    /// </summary>
    public abstract int GetHttpStatusCode();
}
