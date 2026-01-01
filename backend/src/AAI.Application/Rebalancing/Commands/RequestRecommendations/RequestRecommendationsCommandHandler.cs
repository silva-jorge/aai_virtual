using AAI.Application.Common.Interfaces;
using AAI.Application.Rebalancing.DTOs;
using AAI.Domain.Entities;
using AAI.Domain.Enums;
using AAI.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AAI.Application.Rebalancing.Commands.RequestRecommendations;

public class RequestRecommendationsCommandHandler : IRequestHandler<RequestRecommendationsCommand, IEnumerable<RecommendationDto>>
{
    private readonly IPortfolioRepository _portfolioRepository;
    private readonly IRecommendationRepository _recommendationRepository;
    private readonly IAIRecommendationService _aiService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<RequestRecommendationsCommandHandler> _logger;

    public RequestRecommendationsCommandHandler(
        IPortfolioRepository portfolioRepository,
        IRecommendationRepository recommendationRepository,
        IAIRecommendationService aiService,
        IUnitOfWork unitOfWork,
        ILogger<RequestRecommendationsCommandHandler> logger)
    {
        _portfolioRepository = portfolioRepository;
        _recommendationRepository = recommendationRepository;
        _aiService = aiService;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<IEnumerable<RecommendationDto>> Handle(RequestRecommendationsCommand request, CancellationToken cancellationToken)
    {
        var userId = Guid.Parse(request.UserId);
        var portfolio = await _portfolioRepository.GetByUserIdWithPositionsAsync(userId, cancellationToken);

        if (portfolio == null)
        {
            throw new KeyNotFoundException($"Portfolio not found for user {request.UserId}");
        }

        // Check if there are already active recommendations
        if (!request.ForceRegenerate)
        {
            var existingRecommendations = await _recommendationRepository.GetActiveByPortfolioIdAsync(portfolio.Id, cancellationToken);
            if (existingRecommendations.Any())
            {
                _logger.LogInformation("Returning existing recommendations for portfolio {PortfolioId}", portfolio.Id);
                return existingRecommendations.Select(MapToDto).ToList();
            }
        }

        // Generate new recommendations using AI
        _logger.LogInformation("Generating new recommendations for portfolio {PortfolioId}", portfolio.Id);
        
        // TODO: Implement AI service call
        // For now, create a simple mock recommendation
        var recommendation = new Recommendation
        {
            Id = Guid.NewGuid(),
            PortfolioId = portfolio.Id,
            ActionType = RecommendationActionType.Rebalancear,
            Title = "Rebalanceamento Sugerido",
            Description = "Seu portfólio está vazio. Considere adicionar ativos para começar a investir.",
            Rationale = "Um portfólio diversificado ajuda a reduzir riscos e maximizar retornos a longo prazo.",
            Priority = Priority.Media,
            Status = RecommendationStatus.Pendente,
            ExpiresAt = DateTime.UtcNow.AddDays(7),
            CreatedAt = DateTime.UtcNow
        };

        await _recommendationRepository.AddAsync(recommendation, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new[] { MapToDto(recommendation) };
    }

    private static RecommendationDto MapToDto(Recommendation r)
    {
        return new RecommendationDto
        {
            Id = r.Id.ToString(),
            PortfolioId = r.PortfolioId.ToString(),
            ActionType = r.ActionType.ToString(),
            Ticker = r.Ticker,
            Quantity = r.Quantity,
            EstimatedValue = r.EstimatedValue,
            Title = r.Title,
            Description = r.Description,
            Rationale = r.Rationale,
            ImpactJson = r.ImpactJson,
            Priority = r.Priority.ToString(),
            Status = r.Status.ToString(),
            AppliedAt = r.AppliedAt,
            RejectedAt = r.RejectedAt,
            RejectionReason = r.RejectionReason,
            ExpiresAt = r.ExpiresAt,
            CreatedAt = r.CreatedAt
        };
    }
}
