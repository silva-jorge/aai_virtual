using AAI.Domain.Common;

namespace AAI.Domain.Entities;

/// <summary>
/// Índice de referência para comparação (Ibovespa, CDI, IPCA+).
/// </summary>
public class Benchmark : BaseEntity
{
    /// <summary>
    /// Nome do benchmark.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Símbolo do benchmark (IBOV, CDI, IPCA).
    /// </summary>
    public string Symbol { get; set; } = string.Empty;

    /// <summary>
    /// Tipo do benchmark.
    /// </summary>
    public BenchmarkType Type { get; set; }

    /// <summary>
    /// Descrição do benchmark.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Indica se o benchmark está ativo.
    /// </summary>
    public bool IsActive { get; set; } = true;

    // Navigation properties
    /// <summary>
    /// Valores históricos do benchmark.
    /// </summary>
    public ICollection<BenchmarkValue> Values { get; set; } = new List<BenchmarkValue>();
}

/// <summary>
/// Tipo de benchmark.
/// </summary>
public enum BenchmarkType
{
    IndiceAcoes = 1,
    RendaFixa = 2,
    Inflacao = 3
}
