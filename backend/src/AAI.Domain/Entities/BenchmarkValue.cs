using AAI.Domain.Common;

namespace AAI.Domain.Entities;

/// <summary>
/// Valores históricos de benchmarks.
/// </summary>
public class BenchmarkValue : BaseEntity
{
    /// <summary>
    /// Referência ao Benchmark.
    /// </summary>
    public Guid BenchmarkId { get; set; }

    /// <summary>
    /// Data do valor.
    /// </summary>
    public DateOnly Date { get; set; }

    /// <summary>
    /// Valor/pontos no dia.
    /// </summary>
    public decimal Value { get; set; }

    /// <summary>
    /// Retorno diário em percentual.
    /// </summary>
    public decimal? DailyReturn { get; set; }

    /// <summary>
    /// Retorno acumulado no ano em percentual.
    /// </summary>
    public decimal? AccumulatedReturn { get; set; }

    // Navigation properties
    /// <summary>
    /// Benchmark ao qual este valor pertence.
    /// </summary>
    public Benchmark? Benchmark { get; set; }
}
