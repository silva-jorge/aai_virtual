using AAI.Domain.Common;
using AAI.Domain.Enums;

namespace AAI.Domain.Entities;

/// <summary>
/// Notícia agregada de fonte externa.
/// </summary>
public class NewsItem : BaseEntity
{
    /// <summary>
    /// Título da notícia.
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Fonte da notícia.
    /// </summary>
    public string Source { get; set; } = string.Empty;

    /// <summary>
    /// URL original da notícia.
    /// </summary>
    public string SourceUrl { get; set; } = string.Empty;

    /// <summary>
    /// Data de publicação da notícia.
    /// </summary>
    public DateTime PublishedAt { get; set; }

    /// <summary>
    /// Conteúdo completo da notícia (opcional).
    /// </summary>
    public string? Content { get; set; }

    /// <summary>
    /// Resumo gerado por IA.
    /// </summary>
    public string? AISummary { get; set; }

    /// <summary>
    /// Sentimento detectado pela IA.
    /// </summary>
    public Sentiment? Sentiment { get; set; }

    /// <summary>
    /// Score de relevância ao portfólio do usuário (0-100).
    /// </summary>
    public decimal? RelevanceScore { get; set; }

    /// <summary>
    /// Ativos relacionados à notícia (JSON array de tickers).
    /// Exemplo: ["PETR4", "VALE3"]
    /// </summary>
    public string? RelatedAssetsJson { get; set; }

    /// <summary>
    /// Indica se a notícia foi lida pelo usuário.
    /// </summary>
    public bool IsRead { get; set; } = false;
}
