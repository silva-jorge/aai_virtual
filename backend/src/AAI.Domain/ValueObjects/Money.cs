namespace AAI.Domain.ValueObjects;

/// <summary>
/// Value Object representando um valor monetário
/// </summary>
public record Money
{
    public decimal Amount { get; init; }
    public string Currency { get; init; }

    public Money(decimal amount, string currency = "BRL")
    {
        if (amount < 0)
            throw new ArgumentException("O valor não pode ser negativo", nameof(amount));

        if (string.IsNullOrWhiteSpace(currency))
            throw new ArgumentException("A moeda é obrigatória", nameof(currency));

        Amount = decimal.Round(amount, 2);
        Currency = currency.ToUpperInvariant();
    }

    public Money Add(Money other)
    {
        if (Currency != other.Currency)
            throw new InvalidOperationException("Não é possível somar valores com moedas diferentes");

        return new Money(Amount + other.Amount, Currency);
    }

    public Money Subtract(Money other)
    {
        if (Currency != other.Currency)
            throw new InvalidOperationException("Não é possível subtrair valores com moedas diferentes");

        return new Money(Amount - other.Amount, Currency);
    }

    public Money Multiply(decimal factor)
    {
        return new Money(Amount * factor, Currency);
    }

    public Money Divide(decimal divisor)
    {
        if (divisor == 0)
            throw new DivideByZeroException("Não é possível dividir por zero");

        return new Money(Amount / divisor, Currency);
    }

    public override string ToString() => $"{Currency} {Amount:N2}";
}
