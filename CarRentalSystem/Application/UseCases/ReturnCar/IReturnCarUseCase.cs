namespace CarRentalSystem.Application.UseCases.ReturnCar;

using CarRentalSystem.Application.DTOs;
using CarRentalSystem.Application.Results;

/// <summary>
/// Interfaz para el caso de uso de devolución de autos.
/// </summary>
public interface IReturnCarUseCase
{
    /// <summary>
    /// Ejecuta el caso de uso de devolución de auto.
    /// </summary>
    Task<Result<ReturnCarResponse>> ExecuteAsync(ReturnCarRequest request, CancellationToken cancellationToken = default);
}
