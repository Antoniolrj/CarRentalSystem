namespace CarRentalSystem.Application.UseCases.ReturnCar;

using CarRentalSystem.Application.DTOs;
using CarRentalSystem.Application.Errors;
using CarRentalSystem.Application.Results;
using CarRentalSystem.Core.Interfaces;

/// <summary>
/// Implementación del caso de uso de devolución de coches.
/// </summary>
public class ReturnCarUseCase : IReturnCarUseCase
{
    private readonly IRentalRepository _rentalRepository;
    private readonly ICarRepository _carRepository;
    private readonly IPricingDomainService _pricingService;

    public ReturnCarUseCase(
        IRentalRepository rentalRepository,
        ICarRepository carRepository,
        IPricingDomainService pricingService)
    {
        _rentalRepository = rentalRepository;
        _carRepository = carRepository;
        _pricingService = pricingService;
    }

    /// <summary>
    /// Ejecuta el caso de uso de devolución de coche.
    /// </summary>
    public async Task<Result<ReturnCarResponse>> ExecuteAsync(ReturnCarRequest request, CancellationToken cancellationToken = default)
    {
        // Buscar el alquiler
        var rental = await _rentalRepository.GetByIdAsync(request.RentalId, cancellationToken);
        if (rental == null)
            return NotFoundError.Resource("Rental", request.RentalId);

        // Validar que no haya sido devuelto ya
        if (rental.IsReturned)
            return BadRequestError.BusinessRule("El alquiler ya ha sido devuelto.");

        // Obtener el auto para conocer su tipo
        var car = await _carRepository.GetByIdAsync(rental.CarId, cancellationToken);
        if (car == null)
            return NotFoundError.Resource("Car", rental.CarId);

        // Calcular recargo si hay atraso
        decimal surcharge = 0;
        if (request.ReturnDate > rental.ExpectedReturnDate)
        {
            var extraDays = (int)(request.ReturnDate - rental.ExpectedReturnDate).TotalDays;
            var surchargePrice = _pricingService.CalculateSurchargePrice(car.Type, extraDays);
            surcharge = surchargePrice.Amount;
        }

        // Procesar la devolución
        rental.Return(request.ReturnDate, surcharge);

        // Actualizar el alquiler
        await _rentalRepository.UpdateAsync(rental, cancellationToken);

        // Marcar el auto como disponible nuevamente
        car.MarkAsAvailable();
        await _carRepository.UpdateAsync(car, cancellationToken);

        // Devolver response con detalles de la devolución
        return new ReturnCarResponse(
            RentalId: rental.Id,
            OriginalRentalPrice: rental.RentalPrice,
            SurchargePrice: rental.SurchargePrice,
            TotalPrice: rental.RentalPrice + rental.SurchargePrice,
            ReturnDate: rental.ActualReturnDate ?? DateTime.Now
        );
    }
}
