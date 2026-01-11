namespace CarRentalSystem.Core.ValueObjects;

/// <summary>
/// Value Object para representar dinero de forma segura y explícita.
/// </summary>
public sealed class Money : IEquatable<Money>
{
    public decimal Amount { get; }

    public Money(decimal amount)
    {
        if (amount < 0)
            throw new ArgumentException("La cantidad no puede ser negativa.", nameof(amount));

        Amount = amount;
    }

    public static Money operator +(Money left, Money right)
    {
        return new Money(left.Amount + right.Amount);
    }

    public static Money operator -(Money left, Money right)
    {
        return new Money(left.Amount - right.Amount);
    }

    public static Money operator *(Money money, decimal multiplier)
    {
        return new Money(money.Amount * multiplier);
    }

    public static bool operator ==(Money left, Money right) => left.Equals(right);
    public static bool operator !=(Money left, Money right) => !left.Equals(right);
    public static bool operator <(Money left, Money right) => left.Amount < right.Amount;
    public static bool operator >(Money left, Money right) => left.Amount > right.Amount;
    public static bool operator <=(Money left, Money right) => left.Amount <= right.Amount;
    public static bool operator >=(Money left, Money right) => left.Amount >= right.Amount;

    public override bool Equals(object? obj) => Equals(obj as Money);

    public bool Equals(Money? other)
    {
        return other is not null && Amount == other.Amount;
    }

    public override int GetHashCode() => Amount.GetHashCode();
    public override string ToString() => $"{Amount:C}";
}
