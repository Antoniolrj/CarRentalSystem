namespace CarRentalSystem.Application.UseCases.RentCar;

using CarRentalSystem.Application.DTOs;
using CarRentalSystem.Application.Results;

/// <summary>
/// Interfaz para el caso de uso de alquiler de coches.
/// </summary>
public interface IRentCarUseCase
{
    /// <summary>
    /// Ejecuta el caso de uso de alquiler de auto.
    /// </summary>
    /// <param name="request">Solicitud con datos del alquiler (clienteId, autoId, días)</param>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>Result con detalles del alquiler creado o error</returns>
    Task<Result<RentCarResponse>> ExecuteAsync(RentCarRequest request, CancellationToken cancellationToken = default);
}
