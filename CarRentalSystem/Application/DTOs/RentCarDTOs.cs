namespace CarRentalSystem.Application.DTOs;

using System.ComponentModel.DataAnnotations;

/// <summary>
/// Request DTO para alquilar un auto.
/// </summary>
public sealed record RentCarRequest(
    [Required(ErrorMessage = "El ID del cliente es requerido.")]
    [StringLength(50, MinimumLength = 1, ErrorMessage = "El ID del cliente debe tener entre 1 y 50 caracteres.")]
    string CustomerId,

    [Required(ErrorMessage = "El ID del auto es requerido.")]
    [StringLength(50, MinimumLength = 1, ErrorMessage = "El ID del auto debe tener entre 1 y 50 caracteres.")]
    string CarId,

    [Range(1, 365, ErrorMessage = "El número de días debe estar entre 1 y 365.")]
    int Days)
{
    public RentCarRequest() : this(string.Empty, string.Empty, 0) { }
}

/// <summary>
/// Response DTO para alquiler exitoso.
/// </summary>
public sealed record RentCarResponse(
    string RentalId,
    string CustomerId,
    string CarId,
    decimal RentalPrice,
    int LoyaltyPointsEarned,
    DateTime RentalDate,
    DateTime ExpectedReturnDate);
