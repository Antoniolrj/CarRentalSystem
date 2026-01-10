namespace CarRentalSystem.Application.UseCases.GetAvailableCars;

using CarRentalSystem.Application.DTOs;
using CarRentalSystem.Application.Results;
using CarRentalSystem.Core.Interfaces;

/// <summary>
/// Implementación del caso de uso de obtener coches disponibles.
/// </summary>
public class GetAvailableCarsUseCase : IGetAvailableCarsUseCase
{
    private readonly ICarRepository _carRepository;

    public GetAvailableCarsUseCase(ICarRepository carRepository)
    {
        _carRepository = carRepository ?? throw new ArgumentNullException(nameof(carRepository));
    }

    /// <summary>
    /// Ejecuta el caso de uso de obtener coches disponibles.
    /// </summary>
    public async Task<Result<IEnumerable<GetCarsResponse>>> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        var cars = await _carRepository.GetAvailableAsync(cancellationToken);

        return cars.Select(c => new GetCarsResponse(
            Id: c.Id,
            Model: c.Model,
            Type: c.Type.ToString(),
            IsAvailable: c.IsAvailable,
            DailyPrice: c.DailyPrice
        )).ToList();
    }
}