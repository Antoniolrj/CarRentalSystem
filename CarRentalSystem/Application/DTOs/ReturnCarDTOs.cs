namespace CarRentalSystem.Application.DTOs;

using System.ComponentModel.DataAnnotations;

/// <summary>
/// Request DTO para devolver un auto.
/// </summary>
public sealed record ReturnCarRequest(
    [Required(ErrorMessage = "El ID del alquiler es requerido.")]
    [StringLength(50, MinimumLength = 1, ErrorMessage = "El ID del alquiler debe tener entre 1 y 50 caracteres.")]
    string RentalId,

    [Required(ErrorMessage = "La fecha de devolución es requerida.")]
    DateTime ReturnDate)
{
    public ReturnCarRequest() : this(string.Empty, DateTime.Now) { }
}

/// <summary>
/// Response DTO para devolución de auto.
/// </summary>
public sealed record ReturnCarResponse(
    string RentalId,
    decimal OriginalRentalPrice,
    decimal SurchargePrice,
    decimal TotalPrice,
    DateTime ReturnDate);
