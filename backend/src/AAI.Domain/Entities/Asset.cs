using AAI.Domain.Common;
using AAI.Domain.Enums;
using AAI.Domain.ValueObjects;

namespace AAI.Domain.Entities;

/// <summary>
/// Representa um ativo investível (ação, ETF, FII, etc).
/// </summary>
public class Asset : BaseEntity
{
    /// <summary>
    /// Código do ativo (ticker).
    /// </summary>
    public string Ticker { get; set; } = string.Empty;

    /// <summary>
    /// Nome do ativo.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Classe do ativo.
    /// </summary>
    public AssetClass AssetClass { get; set; }

    /// <summary>
    /// Bolsa de negociação (B3, NYSE, NASDAQ, etc).
    /// </summary>
    public string? Exchange { get; set; }

    /// <summary>
    /// Setor econômico.
    /// </summary>
    public string? Sector { get; set; }

    /// <summary>
    /// Moeda de cotação (ISO 4217).
    /// </summary>
    public string Currency { get; set; } = "BRL";

    /// <summary>
    /// Preço atual do ativo.
    /// </summary>
    public Money? CurrentPrice { get; set; }

    /// <summary>
    /// Data/hora da última atualização de preço.
    /// </summary>
    public DateTime? LastPriceUpdate { get; set; }

    /// <summary>
    /// Indica se o ativo está ativo para trading.
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Indica se é entrada manual (sem cotação automática).
    /// </summary>
    public bool IsManualEntry { get; set; } = false;

    // Navigation properties
    /// <summary>
    /// Posições que possuem este ativo.
    /// </summary>
    public ICollection<Position> Positions { get; set; } = new List<Position>();
}
