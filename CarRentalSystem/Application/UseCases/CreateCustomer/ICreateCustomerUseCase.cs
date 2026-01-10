namespace CarRentalSystem.Application.UseCases.CreateCustomer;

using CarRentalSystem.Application.DTOs;
using CarRentalSystem.Application.Results;

/// <summary>
/// Interfaz para el caso de uso de crear un nuevo cliente.
/// </summary>
public interface ICreateCustomerUseCase
{
    /// <summary>
    /// Ejecuta el caso de uso de crear un nuevo cliente.
    /// </summary>
    /// <param name="request">Datos del cliente a crear</param>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>Resultado con los datos del cliente creado</returns>
    Task<Result<CreateCustomerResponse>> ExecuteAsync(CreateCustomerRequest request, CancellationToken cancellationToken = default);
}
