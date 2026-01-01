using AAI.Domain.Common;
using AAI.Domain.ValueObjects;

namespace AAI.Domain.Entities;

/// <summary>
/// Representa uma posição em um ativo específico dentro do portfólio.
/// </summary>
public class Position : BaseEntity
{
    /// <summary>
    /// Referência ao Portfolio.
    /// </summary>
    public Guid PortfolioId { get; set; }

    /// <summary>
    /// Referência ao Asset.
    /// </summary>
    public Guid AssetId { get; set; }

    /// <summary>
    /// Quantidade de unidades do ativo.
    /// </summary>
    public decimal Quantity { get; set; }

    /// <summary>
    /// Custo médio de aquisição (preço unitário médio).
    /// </summary>
    public Money AverageCost { get; set; }

    /// <summary>
    /// Valor total investido na posição.
    /// </summary>
    public Money TotalInvested { get; set; }

    /// <summary>
    /// Valor atual da posição (calculado: Quantity * Asset.CurrentPrice).
    /// </summary>
    public Money CurrentValue { get; set; }

    /// <summary>
    /// Percentual de alocação no portfólio (0-100).
    /// </summary>
    public decimal AllocationPercent { get; set; }

    /// <summary>
    /// Ganho ou perda não realizado (CurrentValue - TotalInvested).
    /// </summary>
    public Money UnrealizedGainLoss { get; set; }

    /// <summary>
    /// Percentual de ganho ou perda não realizado.
    /// </summary>
    public Percentage UnrealizedGainLossPercent { get; set; }

    // Navigation properties
    /// <summary>
    /// Portfólio ao qual esta posição pertence.
    /// </summary>
    public Portfolio? Portfolio { get; set; }

    /// <summary>
    /// Ativo desta posição.
    /// </summary>
    public Asset? Asset { get; set; }

    /// <summary>
    /// Transações associadas a esta posição.
    /// </summary>
    public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
