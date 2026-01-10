namespace CarRentalSystem.Application.UseCases.RentCar;

using CarRentalSystem.Application.DTOs;
using CarRentalSystem.Application.Errors;
using CarRentalSystem.Application.Results;
using CarRentalSystem.Core.Entities;
using CarRentalSystem.Core.Interfaces;

/// <summary>
/// Implementación del caso de uso de alquiler de coches.
/// </summary>
public class RentCarUseCase : IRentCarUseCase
{
    private readonly ICarRepository _carRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IRentalRepository _rentalRepository;
    private readonly IPricingDomainService _pricingService;

    public RentCarUseCase(
        ICarRepository carRepository,
        ICustomerRepository customerRepository,
        IRentalRepository rentalRepository,
        IPricingDomainService pricingService)
    {
        _carRepository = carRepository;
        _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
        _rentalRepository = rentalRepository ?? throw new ArgumentNullException(nameof(rentalRepository));
        _pricingService = pricingService ?? throw new ArgumentNullException(nameof(pricingService));
    }

    /// <summary>
    /// Ejecuta el caso de uso de alquiler de auto.
    /// Orquesta: validación, cálculo de precio, creación de alquiler, actualización de estado.
    /// </summary>
    public async Task<Result<RentCarResponse>> ExecuteAsync(RentCarRequest request, CancellationToken cancellationToken = default)
    {
        // Validar que el coche existe
        var car = await _carRepository.GetByIdAsync(request.CarId, cancellationToken);
        if (car == null)
            return NotFoundError.Resource("Car", request.CarId);

        // Validar que el coche está disponible
        if (!car.IsAvailable)
            return BadRequestError.ResourceUnavailable("El coche no está disponible para alquiler.");

        // Validar que el cliente existe
        var customer = await _customerRepository.GetByIdAsync(request.CustomerId, cancellationToken);
        if (customer == null)
            return NotFoundError.Resource("Customer", request.CustomerId);

        // Calcular precio y puntos de lealtad
        var rentalPrice = _pricingService.CalculateRentalPrice(car.Type, request.Days);
        var loyaltyPoints = _pricingService.GetLoyaltyPoints(car.Type);

        // Crear el alquiler
        var rentalId = Guid.NewGuid().ToString();
        var rentalDate = DateTime.Now;
        var expectedReturnDate = rentalDate.AddDays(request.Days);

        var rental = Rental.Create(
            rentalId,
            request.CustomerId,
            request.CarId,
            rentalDate,
            expectedReturnDate,
            rentalPrice.Amount,
            loyaltyPoints
        );

        // Guardar el alquiler
        await _rentalRepository.AddAsync(rental, cancellationToken);

        // Actualizar el coche como no disponible
        car.MarkAsUnavailable();
        await _carRepository.UpdateAsync(car, cancellationToken);

        // Añadir puntos de lealtad al cliente
        customer.AddLoyaltyPoints(loyaltyPoints);
        await _customerRepository.UpdateAsync(customer, cancellationToken);

        // Retornar response con los detalles del alquiler creado
        return new RentCarResponse(
            RentalId: rental.Id,
            CustomerId: rental.CustomerId,
            CarId: rental.CarId,
            RentalPrice: rental.RentalPrice,
            LoyaltyPointsEarned: rental.LoyaltyPointsEarned,
            RentalDate: rental.RentalDate,
            ExpectedReturnDate: rental.ExpectedReturnDate
        );
    }
}
