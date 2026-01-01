using AAI.Application.Portfolio.DTOs;
using AAI.Domain.Interfaces;
using MediatR;

namespace AAI.Application.Portfolio.Queries.GetPortfolioSummary;

public class GetPortfolioSummaryQueryHandler : IRequestHandler<GetPortfolioSummaryQuery, PortfolioSummaryDto>
{
    private readonly IPortfolioRepository _portfolioRepository;

    public GetPortfolioSummaryQueryHandler(IPortfolioRepository portfolioRepository)
    {
        _portfolioRepository = portfolioRepository;
    }

    public async Task<PortfolioSummaryDto> Handle(GetPortfolioSummaryQuery request, CancellationToken cancellationToken)
    {
        var userId = Guid.Parse(request.UserId);
        var portfolio = await _portfolioRepository.GetByUserIdWithPositionsAsync(userId, cancellationToken);

        if (portfolio == null)
        {
            throw new KeyNotFoundException($"Portfolio not found for user {request.UserId}");
        }

        var totalInvested = portfolio.Positions.Sum(p => p.TotalInvested.Amount);
        var currentValue = portfolio.Positions.Sum(p => p.CurrentValue.Amount);
        var totalGainLoss = currentValue - totalInvested;
        var totalGainLossPercent = totalInvested > 0 ? (totalGainLoss / totalInvested) * 100 : 0;

        return new PortfolioSummaryDto
        {
            Id = portfolio.Id.ToString(),
            Name = portfolio.Name,
            Description = portfolio.Description,
            Currency = portfolio.Currency,
            TotalInvested = totalInvested,
            CurrentValue = currentValue,
            TotalGainLoss = totalGainLoss,
            TotalGainLossPercent = totalGainLossPercent,
            PositionsCount = portfolio.Positions.Count,
            AssetsCount = portfolio.Positions.Select(p => p.AssetId).Distinct().Count(),
            CreatedAt = portfolio.CreatedAt,
            UpdatedAt = portfolio.UpdatedAt ?? portfolio.CreatedAt
        };
    }
}
