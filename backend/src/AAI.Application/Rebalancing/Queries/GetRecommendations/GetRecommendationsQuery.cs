using AAI.Application.Rebalancing.DTOs;
using MediatR;

namespace AAI.Application.Rebalancing.Queries.GetRecommendations;

public record GetRecommendationsQuery(string UserId) : IRequest<IEnumerable<RecommendationDto>>;
