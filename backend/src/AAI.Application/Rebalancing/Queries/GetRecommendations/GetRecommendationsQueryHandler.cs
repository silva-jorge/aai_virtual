using AAI.Application.Rebalancing.DTOs;
using AAI.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AAI.Application.Rebalancing.Queries.GetRecommendations;

public class GetRecommendationsQueryHandler : IRequestHandler<GetRecommendationsQuery, IEnumerable<RecommendationDto>>
{
    private readonly IPortfolioRepository _portfolioRepository;
    private readonly IRecommendationRepository _recommendationRepository;
    private readonly IMapper _mapper;

    public GetRecommendationsQueryHandler(
        IPortfolioRepository portfolioRepository,
        IRecommendationRepository recommendationRepository,
        IMapper mapper)
    {
        _portfolioRepository = portfolioRepository;
        _recommendationRepository = recommendationRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<RecommendationDto>> Handle(GetRecommendationsQuery request, CancellationToken cancellationToken)
    {
        var userId = Guid.Parse(request.UserId);
        var portfolio = await _portfolioRepository.GetByUserIdAsync(userId, cancellationToken);

        if (portfolio == null)
        {
            throw new KeyNotFoundException($"Portfolio not found for user {request.UserId}");
        }

        var recommendations = await _recommendationRepository.GetActiveByPortfolioIdAsync(portfolio.Id, cancellationToken);

        return recommendations.Select(r => new RecommendationDto
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
        }).ToList();
    }
}
