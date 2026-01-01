using AAI.Domain.Common;
using AAI.Domain.Enums;
using AAI.Domain.ValueObjects;

namespace AAI.Domain.Entities;

/// <summary>
/// Registro de operação de compra ou venda.
/// </summary>
public class Transaction : BaseEntity
{
    /// <summary>
    /// Referência à Position.
    /// </summary>
    public Guid PositionId { get; set; }

    /// <summary>
    /// Tipo de transação.
    /// </summary>
    public TransactionType TransactionType { get; set; }

    /// <summary>
    /// Quantidade transacionada.
    /// </summary>
    public decimal Quantity { get; set; }

    /// <summary>
    /// Preço unitário da transação.
    /// </summary>
    public Money UnitPrice { get; set; }

    /// <summary>
    /// Valor total da transação.
    /// </summary>
    public Money TotalValue { get; set; }

    /// <summary>
    /// Taxas e custos da operação.
    /// </summary>
    public Money Fees { get; set; }

    /// <summary>
    /// Data da transação.
    /// </summary>
    public DateTime TransactionDate { get; set; }

    /// <summary>
    /// Corretora utilizada (opcional).
    /// </summary>
    public string? Broker { get; set; }

    /// <summary>
    /// Observações sobre a transação (opcional).
    /// </summary>
    public string? Notes { get; set; }

    // Navigation properties
    /// <summary>
    /// Posição associada a esta transação.
    /// </summary>
    public Position? Position { get; set; }
}
