namespace CP4.Domain.ValueObject;


public sealed class Money : IEquatable<Money>
{
    public decimal Amount { get; }
    public string Currency { get; }

    public static Money Zero => new Money(0);
    
    public Money(decimal amount, string currency = "BRL")
    {
        if (amount < 0)
            throw new ArgumentException("O valor monetário não pode ser negativo.", nameof(amount));

        Amount = decimal.Round(amount, 2, MidpointRounding.ToEven);
        Currency = currency;
    }

    
    public static Money From(decimal value, string currency = "BRL")
        => new Money(value, currency);
    
    public static Money operator +(Money a, Money b)
    {
        EnsureSameCurrency(a, b);
        return new Money(a.Amount + b.Amount, a.Currency);
    }

    public static Money operator -(Money a, Money b)
    {
        EnsureSameCurrency(a, b);
        return new Money(a.Amount - b.Amount, a.Currency);
    }
    
    public static Money operator *(Money money, int multiplier)
    {
        if (multiplier < 0)
            throw new ArgumentException("Quantidade inválida");

        return new Money(money.Amount * multiplier, money.Currency);
    }

    public static bool operator >(Money a, Money b)
    {
        EnsureSameCurrency(a, b);
        return a.Amount > b.Amount;
    }

    public static bool operator <(Money a, Money b)
    {
        EnsureSameCurrency(a, b);
        return a.Amount < b.Amount;
    }

    public static bool operator >=(Money a, Money b) => !(a < b);
    public static bool operator <=(Money a, Money b) => !(a > b);

    
    public bool Equals(Money? other)
    {
        if (other is null) return false;
        return Amount == other.Amount && Currency == other.Currency;
    }

    public override bool Equals(object? obj) => Equals(obj as Money);
    public override int GetHashCode() => HashCode.Combine(Amount, Currency);
    
    private static void EnsureSameCurrency(Money a, Money b)
    {
        if (a.Currency != b.Currency)
            throw new InvalidOperationException("Não é possível operar entre moedas diferentes.");
    }

    public override string ToString() => $"{Currency} {Amount:N2}";
}
