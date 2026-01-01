using AAI.Application.Portfolio.DTOs;
using AAI.Domain.Interfaces;
using MediatR;

namespace AAI.Application.Portfolio.Queries.GetAllocationBreakdown;

public class GetAllocationBreakdownQueryHandler : IRequestHandler<GetAllocationBreakdownQuery, AllocationBreakdownDto>
{
    private readonly IPortfolioRepository _portfolioRepository;

    public GetAllocationBreakdownQueryHandler(IPortfolioRepository portfolioRepository)
    {
        _portfolioRepository = portfolioRepository;
    }

    public async Task<AllocationBreakdownDto> Handle(GetAllocationBreakdownQuery request, CancellationToken cancellationToken)
    {
        var userId = Guid.Parse(request.UserId);
        var portfolio = await _portfolioRepository.GetByUserIdWithPositionsAsync(userId, cancellationToken);

        if (portfolio == null)
        {
            throw new KeyNotFoundException($"Portfolio not found for user {request.UserId}");
        }

        var totalValue = portfolio.Positions.Sum(p => p.CurrentValue.Amount);

        // Group by asset class
        var byAssetClass = portfolio.Positions
            .GroupBy(p => p.Asset!.AssetClass)
            .Select(g => new AssetClassAllocationDto
            {
                AssetClass = g.Key.ToString(),
                Value = g.Sum(p => p.CurrentValue.Amount),
                Percent = totalValue > 0 ? (g.Sum(p => p.CurrentValue.Amount) / totalValue) * 100 : 0,
                PositionsCount = g.Count()
            })
            .OrderByDescending(a => a.Value)
            .ToList();

        // Top 5 positions
        var topPositions = portfolio.Positions
            .OrderByDescending(p => p.CurrentValue.Amount)
            .Take(5)
            .Select(p => new PositionDto
            {
                Id = p.Id.ToString(),
                AssetId = p.AssetId.ToString(),
                Ticker = p.Asset!.Ticker,
                AssetName = p.Asset.Name,
                AssetClass = p.Asset.AssetClass.ToString(),
                Quantity = p.Quantity,
                AverageCost = p.AverageCost.Amount,
                CurrentPrice = p.Asset.CurrentPrice?.Amount ?? 0,
                TotalInvested = p.TotalInvested.Amount,
                CurrentValue = p.CurrentValue.Amount,
                AllocationPercent = p.AllocationPercent,
                GainLoss = p.UnrealizedGainLoss.Amount,
                GainLossPercent = p.UnrealizedGainLossPercent.Value
            })
            .ToList();

        return new AllocationBreakdownDto
        {
            ByAssetClass = byAssetClass,
            TopPositions = topPositions
        };
    }
}
