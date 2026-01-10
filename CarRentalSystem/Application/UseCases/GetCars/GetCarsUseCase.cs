namespace CarRentalSystem.Application.UseCases.GetCars;

using CarRentalSystem.Application.DTOs;
using CarRentalSystem.Application.Results;
using CarRentalSystem.Core.Interfaces;

/// <summary>
/// Caso de uso de obtener todos los coches.
/// </summary>
public class GetCarsUseCase : IGetCarsUseCase
{
    private readonly ICarRepository _carRepository;

    public GetCarsUseCase(ICarRepository carRepository)
    {
        _carRepository = carRepository;
    }
    /// <summary>
    /// Ejecuta el caso de uso de obtener todos los coches.
    /// </summary>
    public async Task<Result<IEnumerable<GetCarsResponse>>> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        var cars = await _carRepository.GetAllAsync(cancellationToken);

        return cars.Select(c => new GetCarsResponse(
            Id: c.Id,
            Model: c.Model,
            Type: c.Type.ToString(),
            IsAvailable: c.IsAvailable,
            DailyPrice: c.DailyPrice
        )).ToList();
    }
}
