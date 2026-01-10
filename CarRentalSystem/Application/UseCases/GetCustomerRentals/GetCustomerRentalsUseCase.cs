namespace CarRentalSystem.Application.UseCases.GetCustomerRentals;

using CarRentalSystem.Application.DTOs;
using CarRentalSystem.Application.Errors;
using CarRentalSystem.Application.Results;
using CarRentalSystem.Core.Interfaces;

/// <summary>
/// Implementación del caso de uso de obtener alquileres de un cliente.
/// </summary>
public class GetCustomerRentalsUseCase : IGetCustomerRentalsUseCase
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IRentalRepository _rentalRepository;

    public GetCustomerRentalsUseCase(
        ICustomerRepository customerRepository,
        IRentalRepository rentalRepository)
    {
        _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
        _rentalRepository = rentalRepository ?? throw new ArgumentNullException(nameof(rentalRepository));
    }

    /// <summary>
    /// Ejecuta el caso de uso de obtener los coches alquilados de un cliente.
    /// </summary>
    public async Task<Result<IEnumerable<RentalDetailsResponse>>> ExecuteAsync(string customerId, CancellationToken cancellationToken = default)
    {
        // Validar que el cliente existe
        var customerExists = await _customerRepository.ExistsAsync(customerId, cancellationToken);
        if (!customerExists)
            return NotFoundError.Resource("Customer", customerId);

        // Obtener alquileres del cliente
        var rentals = await _rentalRepository.GetByCustomerIdAsync(customerId, cancellationToken);

        return rentals.Select(r => new RentalDetailsResponse(
            Id: r.Id,
            CustomerId: r.CustomerId,
            CarId: r.CarId,
            RentalDate: r.RentalDate,
            ExpectedReturnDate: r.ExpectedReturnDate,
            ActualReturnDate: r.ActualReturnDate,
            RentalPrice: r.RentalPrice,
            SurchargePrice: r.SurchargePrice,
            LoyaltyPointsEarned: r.LoyaltyPointsEarned,
            IsReturned: r.IsReturned
        )).ToList();
    }
}
