namespace CarRentalSystem.Core.Entities;

/// <summary>
/// Enumeración de tipos de vehículos.
/// </summary>
public enum CarType
{
    Premium = 0,
    SUV = 1,
    Small = 2
}

/// <summary>
/// Entidad de dominio: Vehículo
/// Representa y gestiona el estado de un vehículo.
/// </summary>
public sealed class Car
{
    public string Id { get; init; }
    public string Model { get; init; }
    public CarType Type { get; init; }
    public bool IsAvailable { get; private set; }

    // Constructor privado sin parámetros para EF Core
    private Car() { }

    private Car(string id, string model, CarType type, bool isAvailable)
    {
        if (string.IsNullOrWhiteSpace(id))
            throw new ArgumentException("El ID del auto no puede estar vacío.", nameof(id));
        if (string.IsNullOrWhiteSpace(model))
            throw new ArgumentException("El modelo del auto no puede estar vacío.", nameof(model));

        Id = id;
        Model = model;
        Type = type;
        IsAvailable = isAvailable;
    }

    /// <summary>
    /// Factory method para crear un nuevo auto.
    /// </summary>
    public static Car Create(string id, string model, CarType type)
    {
        return new Car(id, model, type, isAvailable: true);
    }

    /// <summary>
    /// Actualiza el estado del auto como no disponible.
    /// </summary>
    public void MarkAsUnavailable()
    {
        IsAvailable = false;
    }

    /// <summary>
    /// Actualiza el estado del auto como disponible.
    /// </summary>
    public void MarkAsAvailable()
    {
        IsAvailable = true;
    }
}
