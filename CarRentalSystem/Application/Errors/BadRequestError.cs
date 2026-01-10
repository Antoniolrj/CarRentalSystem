namespace CarRentalSystem.Application.Errors;

/// <summary>
/// Error de validación o solicitud inválida.
/// HTTP 400 - Bad Request
/// </summary>
public class BadRequestError : BaseError
{
    public Dictionary<string, string[]>? FieldErrors { get; }

    public BadRequestError(string message, Dictionary<string, string[]>? fieldErrors = null, string? details = null)
        : base("BAD_REQUEST", message, details)
    {
        FieldErrors = fieldErrors;
    }

    public override int GetHttpStatusCode() => 400;

    /// <summary>
    /// Factory method para crear un error de validación.
    /// </summary>
    public static BadRequestError Validation(string message, Dictionary<string, string[]>? fieldErrors = null) =>
        new(message, fieldErrors, "Validación fallida");

    /// <summary>
    /// Factory method para crear un error cuando un recurso no está disponible.
    /// </summary>
    public static BadRequestError ResourceUnavailable(string message, string? details = null) =>
        new(message, details: details ?? "El recurso no está disponible para esta operación");

    /// <summary>
    /// Factory method para crear un error de negocio.
    /// </summary>
    public static BadRequestError BusinessRule(string message, string? details = null) =>
        new(message, details: details);
}
