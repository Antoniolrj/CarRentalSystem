using CarRentalSystem.Application.DTOs;
using CarRentalSystem.Application.UseCases.CreateCustomer;
using CarRentalSystem.Application.UseCases.GetLoyaltyPoints;
using CarRentalSystem.Presentation.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalSystem.Presentation.Controllers;

/// <summary>
/// Controlador para gestionar operaciones relacionadas con clientes.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class CustomerController : ControllerBase
{
    private readonly ICreateCustomerUseCase _createCustomerUseCase;
    private readonly IGetLoyaltyPointsUseCase _getLoyaltyPointsUseCase;

    public CustomerController(
        ICreateCustomerUseCase createCustomerUseCase,
        IGetLoyaltyPointsUseCase getLoyaltyPointsUseCase)
    {
        _createCustomerUseCase = createCustomerUseCase;
        _getLoyaltyPointsUseCase = getLoyaltyPointsUseCase;
    }

    /// <summary>
    /// Crea un nuevo cliente.
    /// POST /api/customers
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(CreateCustomerResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateCustomer(CreateCustomerRequest request)
    {
        var result = await _createCustomerUseCase.ExecuteAsync(request);

        if (!result.IsSuccess)
            return result.Error!.ToObjectResult();

        return Ok(result.Data);
    }

    /// <summary>
    /// Obtiene los loyalty points de un cliente.
    /// </summary>
    [HttpGet("{customerId}/loyalty-points")]
    [ProducesResponseType(typeof(GetLoyaltyPointsResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetLoyaltyPointsAsync(string customerId)
    {
        var result = await _getLoyaltyPointsUseCase.ExecuteAsync(customerId);
        return result.IsSuccess ? Ok(result.Data) : result.Error!.ToObjectResult();
    }
}
