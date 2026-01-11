namespace CarRentalSystem.Application.UseCases.GetAvailableCars;

using CarRentalSystem.Application.DTOs;
using CarRentalSystem.Application.Results;

/// <summary>
/// Interfaz para el caso de uso de obtener autos disponibles.
/// </summary>
public interface IGetAvailableCarsUseCase
{
    /// <summary>
    /// Ejecuta el caso de uso de obtener autos disponibles.
    /// </summary>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>Result con lista de autos disponibles</returns>
    Task<Result<IEnumerable<GetCarsResponse>>> ExecuteAsync(CancellationToken cancellationToken = default);
}
