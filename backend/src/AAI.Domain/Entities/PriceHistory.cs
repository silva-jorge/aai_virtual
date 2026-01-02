using AAI.Domain.Common;

namespace AAI.Domain.Entities;

/// <summary>
/// Histórico de preços de ativos.
/// </summary>
public class PriceHistory : BaseEntity
{
    /// <summary>
    /// Referência ao Asset.
    /// </summary>
    public Guid AssetId { get; set; }

    /// <summary>
    /// Data da cotação.
    /// </summary>
    public DateOnly Date { get; set; }

    /// <summary>
    /// Preço de abertura.
    /// </summary>
    public decimal OpenPrice { get; set; }

    /// <summary>
    /// Preço máximo do dia.
    /// </summary>
    public decimal HighPrice { get; set; }

    /// <summary>
    /// Preço mínimo do dia.
    /// </summary>
    public decimal LowPrice { get; set; }

    /// <summary>
    /// Preço de fechamento.
    /// </summary>
    public decimal ClosePrice { get; set; }

    /// <summary>
    /// Preço de fechamento ajustado (splits, dividendos, etc).
    /// </summary>
    public decimal AdjustedClose { get; set; }

    /// <summary>
    /// Volume negociado.
    /// </summary>
    public long Volume { get; set; }

    // Navigation properties
    /// <summary>
    /// Ativo ao qual este histórico pertence.
    /// </summary>
    public Asset? Asset { get; set; }
}
