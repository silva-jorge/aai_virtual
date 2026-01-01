namespace AAI.Application.Rebalancing.DTOs;

/// <summary>
/// DTO para uma recomendação de rebalanceamento
/// </summary>
public class RecommendationDto
{
    public string Id { get; set; } = string.Empty;
    public string PortfolioId { get; set; } = string.Empty;
    public string ActionType { get; set; } = string.Empty;
    public string? Ticker { get; set; }
    public decimal? Quantity { get; set; }
    public decimal? EstimatedValue { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Rationale { get; set; } = string.Empty;
    public string? ImpactJson { get; set; }
    public string Priority { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public DateTime? AppliedAt { get; set; }
    public DateTime? RejectedAt { get; set; }
    public string? RejectionReason { get; set; }
    public DateTime? ExpiresAt { get; set; }
    public DateTime CreatedAt { get; set; }
}

/// <summary>
/// DTO para solicitar novas recomendações
/// </summary>
public class RequestRecommendationsDto
{
    public bool ForceRegenerate { get; set; } = false;
}

/// <summary>
/// DTO para aplicar uma recomendação
/// </summary>
public class ApplyRecommendationDto
{
    public string RecommendationId { get; set; } = string.Empty;
}

/// <summary>
/// DTO para rejeitar uma recomendação
/// </summary>
public class RejectRecommendationDto
{
    public string RecommendationId { get; set; } = string.Empty;
    public string? Reason { get; set; }
}
