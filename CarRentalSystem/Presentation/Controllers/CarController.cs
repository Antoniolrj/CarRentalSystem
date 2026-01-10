using CarRentalSystem.Application.DTOs;
using CarRentalSystem.Application.UseCases.GetAvailableCars;
using CarRentalSystem.Application.UseCases.GetCars;
using CarRentalSystem.Application.UseCases.GetCarById;
using CarRentalSystem.Application.UseCases.CreateCar;
using CarRentalSystem.Presentation.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalSystem.Presentation.Controllers;

/// <summary>
/// Controlador para manejar las solicitudes HTTP relacionadas con la flota de coches.
/// </summary>
[ApiController]
[Route("api/cars")]
public class CarController : ControllerBase
{
    private readonly IGetCarsUseCase _getCarsUseCase;
    private readonly IGetCarByIdUseCase _getCarByIdUseCase;
    private readonly IGetAvailableCarsUseCase _getAvailableCarsUseCase;
    private readonly ICreateCarUseCase _createCarUseCase;

    public CarController(
        IGetCarsUseCase getCarsUseCase,
        IGetCarByIdUseCase getCarByIdUseCase,
        IGetAvailableCarsUseCase getAvailableCarsUseCase,
        ICreateCarUseCase createCarUseCase)
    {
        _getCarsUseCase = getCarsUseCase;
        _getCarByIdUseCase = getCarByIdUseCase;
        _getAvailableCarsUseCase = getAvailableCarsUseCase;
        _createCarUseCase = createCarUseCase;
    }

    /// <summary>
    /// Obtiene todos los coches de la flota.
    /// GET /api/cars
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<GetCarsResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllCarsAsync()
    {
        var result = await _getCarsUseCase.ExecuteAsync();
        return result.IsSuccess ? Ok(result.Data) : result.Error!.ToObjectResult();
    }

    /// <summary>
    /// Obtiene un coche específico por ID.
    /// GET /api/cars/{carId}
    /// </summary>
    [HttpGet("{carId}")]
    [ProducesResponseType(typeof(GetCarsResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCarByIdAsync(string carId)
    {
        var result = await _getCarByIdUseCase.ExecuteAsync(carId);
        return result.IsSuccess ? Ok(result.Data) : result.Error!.ToObjectResult();
    }

    /// <summary>
    /// Obtiene solo los autos disponibles para alquilar.
    /// GET /api/cars/available
    /// </summary>
    [HttpGet("available")]
    [ProducesResponseType(typeof(IEnumerable<GetCarsResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAvailableCars()
    {
        var result = await _getAvailableCarsUseCase.ExecuteAsync();
        return result.IsSuccess ? Ok(result.Data) : result.Error!.ToObjectResult();
    }

    /// <summary>
    /// Crea un nuevo coche en la flota.
    /// POST /api/cars/create
    /// </summary>
    [HttpPost("create")]
    [ProducesResponseType(typeof(CreateCarResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateCar(CreateCarRequest request)
    {
        var result = await _createCarUseCase.ExecuteAsync(request);

        return result.IsSuccess ? Ok(result.Data) : result.Error!.ToObjectResult();
    }
}
