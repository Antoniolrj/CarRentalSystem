namespace CarRentalSystem.Application.Services;

using CarRentalSystem.Core.Entities;
using CarRentalSystem.Core.Interfaces;
using CarRentalSystem.Core.ValueObjects;

/// <summary>
/// Implementación del servicio de dominio de precios.
/// Encapsula todas las reglas de negocio relacionadas con precios.
/// Principio SOLID - SRP: Solo responsable de cálculos de precio.
/// Principio SOLID - OCP: Puede extenderse para nuevas reglas sin modificar el código existente.
/// </summary>
public class PricingDomainService : IPricingDomainService
{
    // Precios base constantes
    private const decimal PremiumDailyPrice = 300m;
    private const decimal SuvDailyPrice = 150m;
    private const decimal SmallDailyPrice = 50m;

    public Money CalculateRentalPrice(CarType carType, int days)
    {
        if (days <= 0)
            throw new ArgumentException("El número de días debe ser mayor a 0.", nameof(days));

        return carType switch
        {
            CarType.Premium => CalculatePremiumPrice(days),
            CarType.SUV => CalculateSuvPrice(days),
            CarType.Small => CalculateSmallPrice(days),
            _ => throw new ArgumentException($"Tipo de coche desconocido: {carType}", nameof(carType))
        };
    }

    public Money CalculateSurchargePrice(CarType carType, int extraDays)
    {
        if (extraDays <= 0)
            throw new ArgumentException("El número de días extra debe ser mayor a 0.", nameof(extraDays));

        return carType switch
        {
            CarType.Premium => CalculatePremiumSurcharge(extraDays),
            CarType.SUV => CalculateSuvSurcharge(extraDays),
            CarType.Small => CalculateSmallSurcharge(extraDays),
            _ => throw new ArgumentException($"Tipo de auto desconocido: {carType}", nameof(carType))
        };
    }

    public int GetLoyaltyPoints(CarType carType)
    {
        return carType switch
        {
            CarType.Premium => 5,
            CarType.SUV => 3,
            CarType.Small => 1,
            _ => throw new ArgumentException($"Tipo de auto desconocido: {carType}", nameof(carType))
        };
    }

    /// <summary>
    /// Premium: Precio base × días
    /// </summary>
    private Money CalculatePremiumPrice(int days)
    {
        return new Money(PremiumDailyPrice * days);
    }

    /// <summary>
    /// SUV:
    /// - 1-7 días: 150€/día
    /// - 8-30 días: 80% de 150€ = 120€/día
    /// - +30 días: 50% de 150€ = 75€/día
    /// </summary>
    private Money CalculateSuvPrice(int days)
    {
        if (days <= 7)
        {
            return new Money(SuvDailyPrice * days);
        }
        else if (days <= 30)
        {
            return new Money(SuvDailyPrice * 0.8m * days);
        }
        else
        {
            return new Money(SuvDailyPrice * 0.5m * days);
        }
    }

    /// <summary>
    /// Small:
    /// - 1-7 días: 50€/día
    /// - +7 días: 60% de 50€ = 30€/día
    /// </summary>
    private Money CalculateSmallPrice(int days)
    {
        if (days <= 7)
        {
            return new Money(SmallDailyPrice * days);
        }
        else
        {
            return new Money(SmallDailyPrice * 0.6m * days);
        }
    }

    /// <summary>
    /// Premium recargo: 300€ + 20% de 300€ = 360€/día
    /// </summary>
    private Money CalculatePremiumSurcharge(int extraDays)
    {
        var dailySurcharge = PremiumDailyPrice + (PremiumDailyPrice * 0.2m);
        return new Money(dailySurcharge * extraDays);
    }

    /// <summary>
    /// SUV recargo: 150€ + 60% de 50€ (small price) = 180€/día
    /// </summary>
    private Money CalculateSuvSurcharge(int extraDays)
    {
        var dailySurcharge = SuvDailyPrice + (SmallDailyPrice * 0.6m);
        return new Money(dailySurcharge * extraDays);
    }

    /// <summary>
    /// Small recargo: 50€ + 30% de 50€ = 65€/día
    /// </summary>
    private Money CalculateSmallSurcharge(int extraDays)
    {
        var dailySurcharge = SmallDailyPrice + (SmallDailyPrice * 0.3m);
        return new Money(dailySurcharge * extraDays);
    }
}
