namespace CarRentalSystem.Application.UseCases.GetCarById;

using CarRentalSystem.Application.DTOs;
using CarRentalSystem.Application.Errors;
using CarRentalSystem.Application.Results;
using CarRentalSystem.Core.Interfaces;

/// <summary>
/// Implementación del caso de uso de obtener un coche por ID.
/// </summary>
public class GetCarByIdUseCase : IGetCarByIdUseCase
{
    private readonly ICarRepository _carRepository;

    public GetCarByIdUseCase(ICarRepository carRepository)
    {
        _carRepository = carRepository;
    }

    /// <summary>
    /// Ejecuta el caso de uso para obtener un coche por su ID.
    /// </summary>
    public async Task<Result<GetCarsResponse>> ExecuteAsync(string carId, CancellationToken cancellationToken = default)
    {
        // Validar que el ID no sea vacío
        if (string.IsNullOrWhiteSpace(carId))
            return BadRequestError.Validation("El ID del coche no puede estar vacío.");

        // Obtener el coche
        var car = await _carRepository.GetByIdAsync(carId, cancellationToken);
        if (car == null)
            return NotFoundError.Resource("Car", carId);

        return new GetCarsResponse(
            Id: car.Id,
            Model: car.Model,
            Type: car.Type.ToString(),
            IsAvailable: car.IsAvailable,
            DailyPrice: car.DailyPrice
        );
    }
}
