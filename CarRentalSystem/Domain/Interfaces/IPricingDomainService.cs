namespace CarRentalSystem.Domain.Interfaces;

using CarRentalSystem.Domain.Entities;
using CarRentalSystem.Domain.ValueObjects;

/// <summary>
/// Servicio de dominio para cálculos de precios.
/// </summary>
public interface IPricingDomainService
{
    /// <summary>
    /// Calcula el precio del alquiler basado en tipo de auto y días.
    /// </summary>
    Money CalculateRentalPrice(CarType carType, int days);

    /// <summary>
    /// Calcula el recargo por devolución tardía.
    /// </summary>
    Money CalculateSurchargePrice(CarType carType, int extraDays);

    /// <summary>
    /// Obtiene los puntos de lealtad basado en el tipo de auto.
    /// </summary>
    int GetLoyaltyPoints(CarType carType);
}
