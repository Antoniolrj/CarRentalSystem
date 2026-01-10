namespace CarRentalSystem.Core.ValueObjects;

/// <summary>
/// Value Object para representar una identificación de auto.
/// Principio SOLID - SRP: Encapsula la lógica de validación de ID.
/// </summary>
public sealed class CarId : IEquatable<CarId>
{
    public string Value { get; }

    public CarId(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("El ID del auto no puede estar vacío.", nameof(value));

        Value = value;
    }

    public override bool Equals(object? obj) => Equals(obj as CarId);

    public bool Equals(CarId? other)
    {
        return other is not null && Value == other.Value;
    }

    public override int GetHashCode() => Value.GetHashCode();
    public override string ToString() => Value;

    public static bool operator ==(CarId left, CarId right) => left.Equals(right);
    public static bool operator !=(CarId left, CarId right) => !left.Equals(right);
}
