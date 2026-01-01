using AAI.Domain.Common;
using AAI.Domain.Enums;

namespace AAI.Domain.Entities;

/// <summary>
/// Representa uma recomendação de rebalanceamento gerada pela IA
/// </summary>
public class Recommendation : BaseEntity
{
    /// <summary>
    /// Referência ao Portfolio
    /// </summary>
    public Guid PortfolioId { get; set; }

    /// <summary>
    /// Tipo de ação recomendada
    /// </summary>
    public RecommendationActionType ActionType { get; set; }

    /// <summary>
    /// Ticker do ativo (se aplicável)
    /// </summary>
    public string? Ticker { get; set; }

    /// <summary>
    /// Quantidade sugerida
    /// </summary>
    public decimal? Quantity { get; set; }

    /// <summary>
    /// Valor estimado da operação
    /// </summary>
    public decimal? EstimatedValue { get; set; }

    /// <summary>
    /// Título da recomendação
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Descrição detalhada
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Justificativa da IA
    /// </summary>
    public string Rationale { get; set; } = string.Empty;

    /// <summary>
    /// Impacto esperado na alocação (JSON)
    /// </summary>
    public string? ImpactJson { get; set; }

    /// <summary>
    /// Prioridade da recomendação
    /// </summary>
    public Priority Priority { get; set; }

    /// <summary>
    /// Status da recomendação
    /// </summary>
    public RecommendationStatus Status { get; set; }

    /// <summary>
    /// Data/hora em que foi aplicada (se aplicável)
    /// </summary>
    public DateTime? AppliedAt { get; set; }

    /// <summary>
    /// Data/hora em que foi rejeitada (se aplicável)
    /// </summary>
    public DateTime? RejectedAt { get; set; }

    /// <summary>
    /// Motivo da rejeição (se aplicável)
    /// </summary>
    public string? RejectionReason { get; set; }

    /// <summary>
    /// Data/hora de expiração
    /// </summary>
    public DateTime? ExpiresAt { get; set; }

    // Navigation properties
    /// <summary>
    /// Portfólio associado
    /// </summary>
    public Portfolio? Portfolio { get; set; }
}
