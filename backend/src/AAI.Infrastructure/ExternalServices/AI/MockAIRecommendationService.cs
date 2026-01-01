using AAI.Application.Common.Interfaces;
using Microsoft.Extensions.Logging;

namespace AAI.Infrastructure.ExternalServices.AI;

/// <summary>
/// Mock implementation of AI Recommendation Service
/// TODO: Integrate with real AI service (Groq, OpenAI, etc)
/// </summary>
public class MockAIRecommendationService : IAIRecommendationService
{
    private readonly ILogger<MockAIRecommendationService> _logger;

    public MockAIRecommendationService(ILogger<MockAIRecommendationService> logger)
    {
        _logger = logger;
    }

    public Task<RecommendationResult> GenerateRecommendationAsync(
        PortfolioAnalysisRequest request, 
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Generating mock recommendations for portfolio");

        var actions = new List<RecommendationAction>();

        // Mock recommendation when portfolio value is low or zero
        if (request.TotalValue < 1000)
        {
            actions.Add(new RecommendationAction(
                Ticker: "IVVB11",
                ActionType: "Comprar",
                Quantity: 10,
                Reasoning: "ETF de baixo custo que replica o S&P 500, ideal para começar com diversificação global."
            ));

            actions.Add(new RecommendationAction(
                Ticker: "TESOURO SELIC",
                ActionType: "Comprar",
                Quantity: 1,
                Reasoning: "Investimento seguro em renda fixa para balancear o portfólio."
            ));
        }

        var result = new RecommendationResult(
            Actions: actions,
            Justification: "Baseado no seu perfil de risco e alocação atual, recomendamos iniciar com investimentos diversificados entre renda variável e fixa.",
            ConfidenceScore: 0.75m
        );

        return Task.FromResult(result);
    }

    public Task<string> AnalyzeMarketEventAsync(
        string eventDescription, 
        IEnumerable<string> affectedTickers, 
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Mock analysis of market event for tickers: {Tickers}", 
            string.Join(", ", affectedTickers));

        return Task.FromResult($"Análise mock: {eventDescription}. " +
            $"Os ativos {string.Join(", ", affectedTickers)} podem ser afetados. " +
            "Em produção, a IA forneceria insights detalhados.");
    }
}
