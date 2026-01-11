namespace CarRentalSystem.Application.UseCases.GetLoyaltyPoints;

using CarRentalSystem.Application.DTOs;
using CarRentalSystem.Application.Errors;
using CarRentalSystem.Application.Results;
using CarRentalSystem.Domain.Interfaces;

/// <summary>
/// Implementación del caso de uso para obtener loyalty points de un cliente.
/// </summary>
public class GetLoyaltyPointsUseCase : IGetLoyaltyPointsUseCase
{
    private readonly ICustomerRepository _customerRepository;

    public GetLoyaltyPointsUseCase(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
    }

    /// <summary>
    /// Ejecuta el caso de uso para obtener loyalty points de un cliente.
    /// </summary>
    public async Task<Result<GetLoyaltyPointsResponse>> ExecuteAsync(string customerId, CancellationToken cancellationToken = default)
    {
        var customer = await _customerRepository.GetByIdAsync(customerId, cancellationToken);
        if (customer == null)
            return NotFoundError.Resource("Customer", customerId);

        return new GetLoyaltyPointsResponse(
            CustomerId: customerId,
            LoyaltyPoints: customer.LoyaltyPoints
        );
    }
}
