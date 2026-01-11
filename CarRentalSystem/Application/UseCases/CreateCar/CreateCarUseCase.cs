namespace CarRentalSystem.Application.UseCases.CreateCar;

using CarRentalSystem.Application.DTOs;
using CarRentalSystem.Application.Results;
using CarRentalSystem.Domain.Entities;
using CarRentalSystem.Domain.Interfaces;

/// <summary>
/// Caso de uso para crear un nuevo coche.
/// </summary>
public class CreateCarUseCase : ICreateCarUseCase
{
    private readonly ICarRepository _carRepository;

    public CreateCarUseCase(ICarRepository carRepository)
    {
        _carRepository = carRepository;
    }

    /// <summary>
    /// Ejecuta el caso de uso de crear un nuevo coche.
    /// </summary>
    public async Task<Result<CreateCarResponse>> ExecuteAsync(CreateCarRequest request, CancellationToken cancellationToken = default)
    {
        string carId = Guid.NewGuid().ToString();

        var car = Car.Create(carId, request.Model, request.GetCarType());

        await _carRepository.AddAsync(car, cancellationToken);

        return car.ToCreateResponse();
    }
}
