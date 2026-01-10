namespace CarRentalSystem.Application.UseCases.GetCustomerRentals;

using CarRentalSystem.Application.DTOs;
using CarRentalSystem.Application.Results;

/// <summary>
/// Interfaz para el caso de uso de obtener alquileres de un cliente.
/// Responsabilidad: Encapsular toda la lógica de obtención de alquileres.
/// Principio SOLID - ISP: Interfaz específica para este caso de uso.
/// </summary>
public interface IGetCustomerRentalsUseCase
{
    /// <summary>
    /// Ejecuta el caso de uso de obtener los alquileres del cliente.
    /// </summary>
    /// <param name="customerId">ID del cliente</param>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>Result con la lista de alquileres del cliente</returns>
    Task<Result<IEnumerable<RentalDetailsResponse>>> ExecuteAsync(string customerId, CancellationToken cancellationToken = default);
}
