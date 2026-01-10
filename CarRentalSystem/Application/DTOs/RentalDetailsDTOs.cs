namespace CarRentalSystem.Application.DTOs;

/// <summary>
/// Response DTO para detalles de un alquiler (alias para RentalResponse).
/// </summary>
public sealed record RentalDetailsResponse(
    string Id,
    string CustomerId,
    string CarId,
    DateTime RentalDate,
    DateTime ExpectedReturnDate,
    DateTime? ActualReturnDate,
    decimal RentalPrice,
    decimal SurchargePrice,
    int LoyaltyPointsEarned,
    bool IsReturned);
