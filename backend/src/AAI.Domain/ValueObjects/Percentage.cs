namespace AAI.Domain.ValueObjects;

/// <summary>
/// Value Object representando uma porcentagem
/// </summary>
public record Percentage
{
    public decimal Value { get; init; }

    public Percentage(decimal value)
    {
        if (value < 0 || value > 100)
            throw new ArgumentException("A porcentagem deve estar entre 0 e 100", nameof(value));

        Value = decimal.Round(value, 4);
    }

    public static Percentage FromDecimal(decimal decimalValue)
    {
        return new Percentage(decimalValue * 100);
    }

    public decimal ToDecimal() => Value / 100;

    public Percentage Add(Percentage other)
    {
        var result = Value + other.Value;
        if (result > 100)
            throw new InvalidOperationException("A soma das porcentagens não pode exceder 100%");

        return new Percentage(result);
    }

    public Percentage Subtract(Percentage other)
    {
        var result = Value - other.Value;
        if (result < 0)
            throw new InvalidOperationException("A subtração das porcentagens não pode resultar em valor negativo");

        return new Percentage(result);
    }

    public bool IsWithinRange(Percentage target, Percentage tolerance)
    {
        var difference = Math.Abs(Value - target.Value);
        return difference <= tolerance.Value;
    }

    public override string ToString() => $"{Value:N2}%";

    public static Percentage Zero => new(0);
    public static Percentage Full => new(100);
}
