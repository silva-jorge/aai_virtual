using AAI.Domain.Common;
using AAI.Domain.Enums;

namespace AAI.Domain.Entities;

/// <summary>
/// Evento de mercado relevante (fato relevante, balanço, indicador).
/// </summary>
public class MarketEvent : BaseEntity
{
    /// <summary>
    /// Tipo de evento.
    /// </summary>
    public MarketEventType EventType { get; set; }

    /// <summary>
    /// Título do evento.
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Descrição completa do evento.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// URL da fonte do evento.
    /// </summary>
    public string? SourceUrl { get; set; }

    /// <summary>
    /// Data de publicação do evento.
    /// </summary>
    public DateTime PublishedAt { get; set; }

    /// <summary>
    /// Ativos afetados pelo evento (JSON array de tickers).
    /// Exemplo: ["PETR4", "VALE3"]
    /// </summary>
    public string? AffectedAssetsJson { get; set; }

    /// <summary>
    /// Análise de impacto gerada por IA.
    /// </summary>
    public string? ImpactAnalysis { get; set; }

    /// <summary>
    /// Severidade do evento.
    /// </summary>
    public Priority Severity { get; set; }

    /// <summary>
    /// Indica se já foi processado pela IA.
    /// </summary>
    public bool IsProcessed { get; set; } = false;

    /// <summary>
    /// Indica se alerta foi enviado ao usuário.
    /// </summary>
    public bool IsAlertSent { get; set; } = false;
}
