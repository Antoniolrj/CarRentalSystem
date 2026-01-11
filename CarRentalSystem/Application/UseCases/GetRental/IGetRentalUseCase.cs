namespace CarRentalSystem.Application.UseCases.GetRental;

using CarRentalSystem.Application.DTOs;
using CarRentalSystem.Application.Results;

/// <summary>
/// Interfaz para el caso de uso de obtener detalles de un alquiler.
/// </summary>
public interface IGetRentalUseCase
{
    /// <summary>
    /// Ejecuta el caso de uso de obtener detalles del alquiler.
    /// </summary>
    /// <param name="rentalId">ID del alquiler</param>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>Result con los detalles del alquiler</returns>
    Task<Result<RentalDetailsResponse>> ExecuteAsync(string rentalId, CancellationToken cancellationToken = default);
}
