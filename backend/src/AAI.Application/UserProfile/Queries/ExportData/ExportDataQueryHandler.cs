using MediatR;
using AAI.Domain.Interfaces;
using AutoMapper;
using AAI.Application.Portfolio.DTOs;
using AAI.Application.Rebalancing.DTOs;
using AAI.Application.UserProfile.DTOs;

namespace AAI.Application.UserProfile.Queries.ExportData;

/// <summary>
/// Handler for ExportDataQuery - retrieves all user data for export
/// </summary>
public class ExportDataQueryHandler : IRequestHandler<ExportDataQuery, ExportDataResponse>
{
    private readonly IUserProfileRepository _userProfileRepository;
    private readonly IPortfolioRepository _portfolioRepository;
    private readonly IPositionRepository _positionRepository;
    private readonly IRecommendationRepository _recommendationRepository;
    private readonly IMapper _mapper;

    public ExportDataQueryHandler(
        IUserProfileRepository userProfileRepository,
        IPortfolioRepository portfolioRepository,
        IPositionRepository positionRepository,
        IRecommendationRepository recommendationRepository,
        IMapper mapper)
    {
        _userProfileRepository = userProfileRepository;
        _portfolioRepository = portfolioRepository;
        _positionRepository = positionRepository;
        _recommendationRepository = recommendationRepository;
        _mapper = mapper;
    }

    public async Task<ExportDataResponse> Handle(ExportDataQuery request, CancellationToken cancellationToken)
    {
        var userId = Guid.Parse(request.UserId);

        // Get user profile
        var userProfile = await _userProfileRepository.GetByIdAsync(userId, cancellationToken);

        // Get portfolio for the user
        var portfolio = await _portfolioRepository.GetByUserIdWithPositionsAsync(userId, cancellationToken);

        // Initialize response
        var response = new ExportDataResponse
        {
            UserProfile = userProfile != null ? _mapper.Map<UserProfileDto>(userProfile) : null,
            ExportedAt = DateTime.UtcNow
        };

        if (portfolio != null)
        {
            // Map portfolio to summary
            var portfolioSummary = new PortfolioSummaryDto
            {
                Id = portfolio.Id.ToString(),
                Name = portfolio.Name,
                Description = portfolio.Description,
                Currency = portfolio.Currency,
                TotalInvested = portfolio.Positions.Sum(p => p.TotalInvested.Amount),
                CurrentValue = portfolio.Positions.Sum(p => p.CurrentValue.Amount),
                PositionsCount = portfolio.Positions.Count,
                AssetsCount = portfolio.Positions.Select(p => p.AssetId).Distinct().Count(),
                CreatedAt = portfolio.CreatedAt,
                UpdatedAt = portfolio.UpdatedAt ?? portfolio.CreatedAt
            };
            
            response.Portfolios.Add(portfolioSummary);

            // Get all positions
            var totalInvested = portfolio.Positions.Sum(p => p.TotalInvested.Amount);
            var currentValue = portfolio.Positions.Sum(p => p.CurrentValue.Amount);
            
            foreach (var position in portfolio.Positions)
            {
                var positionDto = _mapper.Map<PositionDto>(position);
                positionDto.AllocationPercent = totalInvested > 0 
                    ? (positionDto.TotalInvested / totalInvested) * 100 
                    : 0;
                positionDto.GainLoss = positionDto.CurrentValue - positionDto.TotalInvested;
                positionDto.GainLossPercent = positionDto.TotalInvested > 0 
                    ? ((positionDto.CurrentValue - positionDto.TotalInvested) / positionDto.TotalInvested) * 100 
                    : 0;
                response.Positions.Add(positionDto);
            }

            // Get all recommendations for the portfolio
            var recommendations = await _recommendationRepository.GetAllByPortfolioIdAsync(
                portfolio.Id, cancellationToken);
            response.Recommendations = _mapper.Map<List<RecommendationDto>>(recommendations.ToList());
        }

        return response;
    }
}
