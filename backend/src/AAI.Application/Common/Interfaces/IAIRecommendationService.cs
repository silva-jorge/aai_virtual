namespace AAI.Application.Common.Interfaces;

/// <summary>
/// Interface para serviço de recomendações com IA
/// </summary>
public interface IAIRecommendationService
{
    Task<RecommendationResult> GenerateRecommendationAsync(
        PortfolioAnalysisRequest request,
        CancellationToken cancellationToken = default);

    Task<string> AnalyzeMarketEventAsync(
        string eventDescription,
        IEnumerable<string> affectedTickers,
        CancellationToken cancellationToken = default);
}

public record PortfolioAnalysisRequest(
    Dictionary<string, decimal> CurrentAllocation,
    Dictionary<string, decimal> TargetAllocation,
    decimal TotalValue,
    string RiskProfile,
    IEnumerable<string> RecentNews
);

public record RecommendationResult(
    IEnumerable<RecommendationAction> Actions,
    string Justification,
    decimal ConfidenceScore
);

public record RecommendationAction(
    string Ticker,
    string ActionType,
    decimal Quantity,
    string Reasoning
);
