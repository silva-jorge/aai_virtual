using AAI.Application.Portfolio.DTOs;
using AAI.Domain.Interfaces;
using MediatR;

namespace AAI.Application.Portfolio.Queries.GetPerformanceMetrics;

public class GetPerformanceMetricsQueryHandler : IRequestHandler<GetPerformanceMetricsQuery, PerformanceMetricsDto>
{
    private readonly IPortfolioRepository _portfolioRepository;

    public GetPerformanceMetricsQueryHandler(IPortfolioRepository portfolioRepository)
    {
        _portfolioRepository = portfolioRepository;
    }

    public async Task<PerformanceMetricsDto> Handle(GetPerformanceMetricsQuery request, CancellationToken cancellationToken)
    {
        var userId = Guid.Parse(request.UserId);
        var portfolio = await _portfolioRepository.GetByUserIdWithPositionsAsync(userId, cancellationToken);

        if (portfolio == null)
        {
            throw new KeyNotFoundException($"Portfolio not found for user {request.UserId}");
        }

        var totalInvested = portfolio.Positions.Sum(p => p.TotalInvested.Amount);
        var currentValue = portfolio.Positions.Sum(p => p.CurrentValue.Amount);
        var totalReturn = currentValue - totalInvested;
        var totalReturnPercent = totalInvested > 0 ? (totalReturn / totalInvested) * 100 : 0;

        // TODO: Implementar cálculos reais de day/month/year quando tivermos histórico de preços
        // Por enquanto, retornamos valores mockados/zerados
        return new PerformanceMetricsDto
        {
            TotalReturn = totalReturn,
            TotalReturnPercent = totalReturnPercent,
            DayChange = 0,
            DayChangePercent = 0,
            MonthReturn = 0,
            MonthReturnPercent = 0,
            YearReturn = totalReturn, // Assumindo que todo o retorno é do ano
            YearReturnPercent = totalReturnPercent,
            History = new List<PerformanceHistoryDto>()
        };
    }
}
