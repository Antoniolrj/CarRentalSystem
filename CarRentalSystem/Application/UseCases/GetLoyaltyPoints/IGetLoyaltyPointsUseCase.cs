namespace CarRentalSystem.Application.UseCases.GetLoyaltyPoints;

using CarRentalSystem.Application.DTOs;
using CarRentalSystem.Application.Results;

/// <summary>
/// Interfaz para el caso de uso de obtener puntos de lealtad.
/// Responsabilidad: Encapsular toda la lógica de obtención de puntos de lealtad.
/// Principio SOLID - ISP: Interfaz específica para este caso de uso.
/// </summary>
public interface IGetLoyaltyPointsUseCase
{
    /// <summary>
    /// Ejecuta el caso de uso de obtener puntos de lealtad.
    /// </summary>
    /// <param name="customerId">ID del cliente</param>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>Result con los puntos de lealtad del cliente</returns>
    Task<Result<GetLoyaltyPointsResponse>> ExecuteAsync(string customerId, CancellationToken cancellationToken = default);
}
