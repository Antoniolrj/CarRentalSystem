namespace CarRentalSystem.Application.UseCases.CreateCar;

using CarRentalSystem.Application.DTOs;
using CarRentalSystem.Application.Results;

/// <summary>
/// Interfaz para el caso de uso de crear un nuevo auto.
/// </summary>
public interface ICreateCarUseCase
{
    /// <summary>
    /// Ejecuta el caso de uso para crear un nuevo auto.
    /// </summary>
    Task<Result<CreateCarResponse>> ExecuteAsync(CreateCarRequest request, CancellationToken cancellationToken = default);
}
