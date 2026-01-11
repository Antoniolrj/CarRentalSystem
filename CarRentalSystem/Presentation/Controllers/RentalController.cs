using CarRentalSystem.Application.DTOs;
using CarRentalSystem.Application.UseCases.RentCar;
using CarRentalSystem.Application.UseCases.ReturnCar;
using CarRentalSystem.Application.UseCases.GetRental;
using CarRentalSystem.Application.UseCases.GetCustomerRentals;
using CarRentalSystem.Presentation.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalSystem.Presentation.Controllers;

/// <summary>
/// Controlador para gestionar alquileres y devoluciones de coches.
/// </summary>
[ApiController]
[Route("api/rentals")]
public class RentalController : ControllerBase
{
    private readonly IRentCarUseCase _rentCarUseCase;
    private readonly IReturnCarUseCase _returnCarUseCase;
    private readonly IGetRentalUseCase _getRentalUseCase;
    private readonly IGetCustomerRentalsUseCase _getCustomerRentalsUseCase;

    public RentalController(
        IRentCarUseCase rentCarUseCase,
        IReturnCarUseCase returnCarUseCase,
        IGetRentalUseCase getRentalUseCase,
        IGetCustomerRentalsUseCase getCustomerRentalsUseCase)
    {
        _rentCarUseCase = rentCarUseCase;
        _returnCarUseCase = returnCarUseCase;
        _getRentalUseCase = getRentalUseCase;
        _getCustomerRentalsUseCase = getCustomerRentalsUseCase;
    }

    /// <summary>
    /// Alquila uno o varios coches y calcula el precio.
    /// </summary>
    [HttpPost("rent")]
    [ProducesResponseType(typeof(RentCarResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> RentCarAsync(RentCarRequest request)
    {
        var result = await _rentCarUseCase.ExecuteAsync(request);
        return result.IsSuccess ? Ok(result.Data) : result.Error!.ToObjectResult();
    }

    /// <summary>
    /// Devuelve un auto y calcula recargos si hay atraso.
    /// </summary>
    [HttpPost("return")]
    [ProducesResponseType(typeof(ReturnCarResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ReturnCarAsync(ReturnCarRequest request)
    {
        var result = await _returnCarUseCase.ExecuteAsync(request);
        return result.IsSuccess ? Ok(result.Data) : result.Error!.ToObjectResult();
    }

    /// <summary>
    /// Obtiene los detalles de un alquiler específico.
    /// </summary>
    [HttpGet("{rentalId}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetRentalAsync(string rentalId)
    {
        var result = await _getRentalUseCase.ExecuteAsync(rentalId);
        return result.IsSuccess ? Ok(result.Data) : result.Error!.ToObjectResult();
    }

    /// <summary>
    /// Obtiene todos los alquileres de un cliente específico.
    /// </summary>
    [HttpGet("customer/{customerId}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCustomerRentalsAsync(string customerId)
    {
        var result = await _getCustomerRentalsUseCase.ExecuteAsync(customerId);
        return result.IsSuccess ? Ok(result.Data) : result.Error!.ToObjectResult();
    }
}
