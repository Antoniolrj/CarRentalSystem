namespace CarRentalSystem.Application.DTOs;

/// <summary>
/// Response DTO alias para información de un auto (GetCarsResponse).
/// </summary>
public sealed record GetCarsResponse(
    string Id,
    string Model,
    string Type,
    bool IsAvailable,
    decimal DailyPrice);
