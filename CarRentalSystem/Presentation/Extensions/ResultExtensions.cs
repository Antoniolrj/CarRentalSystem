using CarRentalSystem.Application.Errors;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalSystem.Presentation.Extensions;

/// <summary>
/// Extension methods para mapear resultados a respuestas HTTP de forma limpia.
/// </summary>
public static class ResultExtensions
{
    /// <summary>
    /// Mapea un error a una respuesta HTTP apropiada según el tipo de error.
    /// </summary>
    public static IActionResult ToActionResult(this BaseError error, string? path = null) =>
        error switch
        {
            NotFoundError notFound => new NotFoundObjectResult(new
            {
                error = new
                {
                    code = notFound.Code,
                    message = notFound.Message,
                    resourceName = notFound.ResourceName,
                    resourceId = notFound.ResourceId,
                    path
                }
            }),

            BadRequestError badRequest => new BadRequestObjectResult(new
            {
                error = new
                {
                    code = badRequest.Code,
                    message = badRequest.Message,
                    fieldErrors = badRequest.FieldErrors,
                    details = badRequest.Details,
                    path
                }
            }),

            InternalServerError internalError => new ObjectResult(new
            {
                error = new
                {
                    code = internalError.Code,
                    message = internalError.Message,
                    details = internalError.Details,
                    path
                }
            })
            {
                StatusCode = internalError.GetHttpStatusCode()
            },

            _ => new ObjectResult(new
            {
                error = new
                {
                    code = error.Code,
                    message = error.Message,
                    details = error.Details,
                    path
                }
            })
            {
                StatusCode = error.GetHttpStatusCode()
            }
        };

    /// <summary>
    /// Simplifica el manejo de errores en los controllers.
    /// </summary>
    public static ObjectResult ToObjectResult(this BaseError error) =>
        new(new
        {
            error = new
            {
                code = error.Code,
                message = error.Message,
                details = error.Details
            }
        })
        {
            StatusCode = error.GetHttpStatusCode()
        };
}
