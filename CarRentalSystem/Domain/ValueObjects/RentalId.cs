namespace CarRentalSystem.Core.ValueObjects;

public sealed class RentalId : IEquatable<RentalId>
{
    public string Value { get; }

    public RentalId(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("El ID del alquiler no puede estar vacío.", nameof(value));

        Value = value;
    }

    public override bool Equals(object? obj) => Equals(obj as RentalId);

    public bool Equals(RentalId? other)
    {
        return other is not null && Value == other.Value;
    }

    public override int GetHashCode() => Value.GetHashCode();
    public override string ToString() => Value;

    public static bool operator ==(RentalId left, RentalId right) => left.Equals(right);
    public static bool operator !=(RentalId left, RentalId right) => !left.Equals(right);
}
