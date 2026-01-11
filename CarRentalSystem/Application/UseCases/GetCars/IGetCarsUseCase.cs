namespace CarRentalSystem.Application.UseCases.GetCars;

using CarRentalSystem.Application.DTOs;
using CarRentalSystem.Application.Results;

/// <summary>
/// Interfaz para el caso de uso de obtener todos los autos.
/// </summary>
public interface IGetCarsUseCase
{
    /// <summary>
    /// Ejecuta el caso de uso de obtener todos los autos.
    /// </summary>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>Result con lista de todos los autos</returns>
    Task<Result<IEnumerable<GetCarsResponse>>> ExecuteAsync(CancellationToken cancellationToken = default);
}
