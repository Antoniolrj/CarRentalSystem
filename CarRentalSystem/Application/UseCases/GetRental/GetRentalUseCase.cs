namespace CarRentalSystem.Application.UseCases.GetRental;

using CarRentalSystem.Application.DTOs;
using CarRentalSystem.Application.Errors;
using CarRentalSystem.Application.Results;
using CarRentalSystem.Domain.Interfaces;

/// <summary>
/// Implementación del caso de uso de obtener detalles de un alquiler.
/// </summary>
public class GetRentalUseCase : IGetRentalUseCase
{
    private readonly IRentalRepository _rentalRepository;

    public GetRentalUseCase(IRentalRepository rentalRepository)
    {
        _rentalRepository = rentalRepository ?? throw new ArgumentNullException(nameof(rentalRepository));
    }

    /// <summary>
    /// Ejecuta el caso de uso de obtener detalles de un alquiler.
    /// </summary>
    public async Task<Result<RentalDetailsResponse>> ExecuteAsync(string rentalId, CancellationToken cancellationToken = default)
    {
        var rental = await _rentalRepository.GetByIdAsync(rentalId, cancellationToken);

        if (rental == null)
            return NotFoundError.Resource("Rental", rentalId);

        return new RentalDetailsResponse(
            Id: rental.Id,
            CustomerId: rental.CustomerId,
            CarId: rental.CarId,
            RentalDate: rental.RentalDate,
            ExpectedReturnDate: rental.ExpectedReturnDate,
            ActualReturnDate: rental.ActualReturnDate,
            RentalPrice: rental.RentalPrice,
            SurchargePrice: rental.SurchargePrice,
            LoyaltyPointsEarned: rental.LoyaltyPointsEarned,
            IsReturned: rental.IsReturned
        );
    }
}
