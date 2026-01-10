namespace CarRentalSystem.Application.UseCases.ReturnCar;

using CarRentalSystem.Application.DTOs;
using CarRentalSystem.Application.Results;

/// <summary>
/// Interfaz para el caso de uso de devolución de autos.
/// Responsabilidad: Encapsular toda la lógica de devolución de autos.
/// Principio SOLID - ISP: Interfaz específica para este caso de uso.
/// </summary>
public interface IReturnCarUseCase
{
    /// <summary>
    /// Ejecuta el caso de uso de devolución de auto.
    /// </summary>
    /// <param name="request">Solicitud con datos de devolución (rentalId, returnDate)</param>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>Result con detalles de la devolución y recargos si aplican o error</returns>
    Task<Result<ReturnCarResponse>> ExecuteAsync(ReturnCarRequest request, CancellationToken cancellationToken = default);
}
