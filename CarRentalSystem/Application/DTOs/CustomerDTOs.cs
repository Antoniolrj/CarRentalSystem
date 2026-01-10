namespace CarRentalSystem.Application.DTOs;

using CarRentalSystem.Core.Entities;
using System.ComponentModel.DataAnnotations;

/// <summary>
/// Request DTO para crear un nuevo cliente.
/// Validaciones: Nombre requerido entre 1 y 100 caracteres
/// </summary>
public sealed record CreateCustomerRequest(
    [Required(ErrorMessage = "El nombre del cliente es requerido.")]
    [StringLength(100, MinimumLength = 1, ErrorMessage = "El nombre debe tener entre 1 y 100 caracteres.")]
    string Name);

/// <summary>
/// Response DTO para la creación de un cliente.
/// </summary>
public sealed record CreateCustomerResponse(
    string Id,
    string Name);

/// <summary>
/// Response DTO para puntos de lealtad del cliente.
/// </summary>
public sealed record GetLoyaltyPointsResponse(
    string CustomerId,
    int LoyaltyPoints);

/// <summary>
/// Extension para convertir Customer a CustomerDto.
/// Principio: Mantener conversiones centralizadas.
/// </summary>
public static class CustomerDtoExtensions
{
    public static CreateCustomerResponse ToCreateResponse(this Customer customer)
    {
        return new CreateCustomerResponse(
            customer.Id,
            customer.Name);
    }
}
