namespace CarRentalSystem.Application.UseCases.GetCarById;

using CarRentalSystem.Application.DTOs;
using CarRentalSystem.Application.Results;

/// <summary>
/// Interfaz para el caso de uso de obtener un coche por ID.
/// </summary>
public interface IGetCarByIdUseCase
{
    /// <summary>
    /// Ejecuta el caso de uso para obtener un coche por su ID.
    /// </summary>
    Task<Result<GetCarsResponse>> ExecuteAsync(string carId, CancellationToken cancellationToken = default);
}
