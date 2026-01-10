namespace CarRentalSystem.Core.Entities;

/// <summary>
/// Entidad de dominio: Cliente
/// </summary>
public sealed class Customer
{
    public string Id { get; init; }
    public string Name { get; init; }
    public int LoyaltyPoints { get; private set; }

    // Constructor privado sin parámetros para EF Core
    private Customer() { }

    private Customer(string id, string name, int loyaltyPoints = 0)
    {
        if (string.IsNullOrWhiteSpace(id))
            throw new ArgumentException("El ID del cliente no puede estar vacío.", nameof(id));
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("El nombre del cliente no puede estar vacío.", nameof(name));
        if (loyaltyPoints < 0)
            throw new ArgumentException("Los puntos de lealtad no pueden ser negativos.", nameof(loyaltyPoints));

        Id = id;
        Name = name;
        LoyaltyPoints = loyaltyPoints;
    }

    /// <summary>
    /// Factory method para crear un nuevo cliente.
    /// </summary>
    public static Customer Create(string id, string name)
    {
        return new Customer(id, name);
    }

    /// <summary>
    /// Añade loyalty points al cliente.
    /// </summary>
    public void AddLoyaltyPoints(int points)
    {
        if (points < 0)
            throw new ArgumentException("Los puntos a añadir no pueden ser negativos.", nameof(points));

        LoyaltyPoints += points;
    }
}
