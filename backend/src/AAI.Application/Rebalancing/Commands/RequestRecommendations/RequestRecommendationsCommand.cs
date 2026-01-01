using AAI.Application.Rebalancing.DTOs;
using MediatR;

namespace AAI.Application.Rebalancing.Commands.RequestRecommendations;

public class RequestRecommendationsCommand : IRequest<IEnumerable<RecommendationDto>>
{
    public string UserId { get; set; } = string.Empty;
    public bool ForceRegenerate { get; set; } = false;
}
