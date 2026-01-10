namespace CarRentalSystem.Core.ValueObjects;

public sealed class CustomerId : IEquatable<CustomerId>
{
    public string Value { get; }

    public CustomerId(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("El ID del cliente no puede estar vacío.", nameof(value));

        Value = value;
    }

    public override bool Equals(object? obj) => Equals(obj as CustomerId);

    public bool Equals(CustomerId? other)
    {
        return other is not null && Value == other.Value;
    }

    public override int GetHashCode() => Value.GetHashCode();
    public override string ToString() => Value;

    public static bool operator ==(CustomerId left, CustomerId right) => left.Equals(right);
    public static bool operator !=(CustomerId left, CustomerId right) => !left.Equals(right);
}
