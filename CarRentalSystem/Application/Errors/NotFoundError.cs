namespace CarRentalSystem.Application.Errors;

/// <summary>
/// Error cuando un recurso no es encontrado.
/// HTTP 404 - Not Found
/// </summary>
public class NotFoundError : BaseError
{
    public string? ResourceName { get; }
    public string? ResourceId { get; }

    public NotFoundError(string message, string? resourceName = null, string? resourceId = null, string? details = null)
        : base("NOT_FOUND", message, details)
    {
        ResourceName = resourceName;
        ResourceId = resourceId;
    }

    public override int GetHttpStatusCode() => 404;

    /// <summary>
    /// Factory method para crear un error de recurso no encontrado.
    /// </summary>
    public static NotFoundError Resource(string resourceName, string resourceId) =>
        new($"{resourceName} con ID '{resourceId}' no encontrado.", resourceName, resourceId);
}
