namespace CarRentalSystem.Core.Entities;

/// <summary>
/// Entidad de dominio: Alquiler
/// Responsabilidad única: Representar y gestionar el ciclo de vida de un alquiler.
/// Invariantes del dominio:
/// - Una vez creado un alquiler, sus características no pueden cambiar
/// - Un alquiler puede transicionar de activo a devuelto
/// - Las fechas de devolución no pueden ser antes de la fecha de alquiler
/// </summary>
public sealed class Rental
{
    public string Id { get; init; }
    public string CustomerId { get; init; }
    public string CarId { get; init; }
    public DateTime RentalDate { get; init; }
    public DateTime ExpectedReturnDate { get; init; }
    public DateTime? ActualReturnDate { get; private set; }
    public decimal RentalPrice { get; init; }
    public decimal SurchargePrice { get; private set; }
    public int LoyaltyPointsEarned { get; init; }
    public bool IsReturned { get; private set; }

    // Constructor privado sin parámetros para EF Core
    private Rental() { }

    private Rental(
        string id,
        string customerId,
        string carId,
        DateTime rentalDate,
        DateTime expectedReturnDate,
        decimal rentalPrice,
        int loyaltyPointsEarned)
    {
        if (string.IsNullOrWhiteSpace(id))
            throw new ArgumentException("El ID del alquiler no puede estar vacío.", nameof(id));
        if (string.IsNullOrWhiteSpace(customerId))
            throw new ArgumentException("El ID del cliente no puede estar vacío.", nameof(customerId));
        if (string.IsNullOrWhiteSpace(carId))
            throw new ArgumentException("El ID del auto no puede estar vacío.", nameof(carId));
        if (rentalPrice < 0)
            throw new ArgumentException("El precio del alquiler no puede ser negativo.", nameof(rentalPrice));
        if (expectedReturnDate <= rentalDate)
            throw new ArgumentException("La fecha de devolución debe ser posterior a la de alquiler.", nameof(expectedReturnDate));
        if (loyaltyPointsEarned < 0)
            throw new ArgumentException("Los puntos de lealtad no pueden ser negativos.", nameof(loyaltyPointsEarned));

        Id = id;
        CustomerId = customerId;
        CarId = carId;
        RentalDate = rentalDate;
        ExpectedReturnDate = expectedReturnDate;
        RentalPrice = rentalPrice;
        LoyaltyPointsEarned = loyaltyPointsEarned;
        IsReturned = false;
        SurchargePrice = 0;

    }

    /// <summary>
    /// Factory method para crear un nuevo alquiler.
    /// </summary>
    public static Rental Create(
        string id,
        string customerId,
        string carId,
        DateTime rentalDate,
        DateTime expectedReturnDate,
        decimal rentalPrice,
        int loyaltyPointsEarned)
    {
        return new Rental(id, customerId, carId, rentalDate, expectedReturnDate, rentalPrice, loyaltyPointsEarned);
    }

    /// <summary>
    /// Procesa la devolución del alquiler.
    /// </summary>
    public void Return(DateTime returnDate, decimal surchargePrice)
    {
        if (IsReturned)
            throw new InvalidOperationException("El alquiler ya fue devuelto previamente.");
        if (returnDate < RentalDate)
            throw new ArgumentException("La fecha de devolución no puede ser anterior a la de alquiler.", nameof(returnDate));
        if (surchargePrice < 0)
            throw new ArgumentException("El recargo no puede ser negativo.", nameof(surchargePrice));

        ActualReturnDate = returnDate;
        SurchargePrice = surchargePrice;
        IsReturned = true;
    }

    /// <summary>
    /// Calcula si hay atraso en la devolución.
    /// </summary>
    public bool IsLate => ActualReturnDate.HasValue && ActualReturnDate.Value > ExpectedReturnDate;

    /// <summary>
    /// Calcula los días de atraso.
    /// </summary>
    public int DaysLate => IsLate ? (int)(ActualReturnDate!.Value - ExpectedReturnDate).TotalDays : 0;
}
