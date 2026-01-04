using AAI.Domain.Entities;
using AAI.Domain.Enums;
using AAI.Domain.Interfaces;
using AAI.Domain.ValueObjects;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace AAI.Application.UserProfile.Commands.ImportData;

/// <summary>
/// Handler for ImportDataCommand - imports user data from backup
/// </summary>
public class ImportDataCommandHandler : IRequestHandler<ImportDataCommand, ImportDataResult>
{
    private readonly IUserProfileRepository _userProfileRepository;
    private readonly IPortfolioRepository _portfolioRepository;
    private readonly IPositionRepository _positionRepository;
    private readonly IAssetRepository _assetRepository;
    private readonly IRecommendationRepository _recommendationRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IMemoryCache _cache;
    private readonly ILogger<ImportDataCommandHandler> _logger;

    public ImportDataCommandHandler(
        IUserProfileRepository userProfileRepository,
        IPortfolioRepository portfolioRepository,
        IPositionRepository positionRepository,
        IAssetRepository assetRepository,
        IRecommendationRepository recommendationRepository,
        IUnitOfWork _unitOfWork,
        IMapper mapper,
        IMemoryCache cache,
        ILogger<ImportDataCommandHandler> logger)
    {
        _userProfileRepository = userProfileRepository;
        _portfolioRepository = portfolioRepository;
        _positionRepository = positionRepository;
        _assetRepository = assetRepository;
        _recommendationRepository = recommendationRepository;
        this._unitOfWork = _unitOfWork;
        _mapper = mapper;
        _cache = cache;
        _logger = logger;
    }

    public async Task<ImportDataResult> Handle(ImportDataCommand request, CancellationToken cancellationToken)
    {
        var result = new ImportDataResult();
        var userId = Guid.Parse(request.UserId);

        try
        {
            // Start transaction
            await _unitOfWork.BeginTransactionAsync(cancellationToken);

            // 1. Import UserProfile
            if (request.UserProfile != null)
            {
                var existingProfile = await _userProfileRepository.GetByIdAsync(userId, cancellationToken);

                if (existingProfile != null)
                {
                    // Update existing profile
                    if (Enum.TryParse<RiskProfile>(request.UserProfile.RiskProfile, true, out var riskProfile))
                    {
                        existingProfile.RiskProfile = riskProfile;
                    }
                    existingProfile.InvestmentGoal = request.UserProfile.InvestmentGoal;
                    existingProfile.VolatilityTolerance = request.UserProfile.VolatilityTolerance;
                    existingProfile.TimeHorizonMonths = request.UserProfile.TimeHorizonMonths;
                    existingProfile.RebalanceThresholdPercent = request.UserProfile.RebalanceThresholdPercent;
                    existingProfile.TargetAllocationJson = request.UserProfile.TargetAllocationJson ?? "{}";
                    existingProfile.UpdatedAt = DateTime.UtcNow;

                    await _userProfileRepository.UpdateAsync(existingProfile, cancellationToken);
                }
            }

            // 2. Import Portfolios and Positions
            foreach (var portfolioDto in request.Portfolios)
            {
                var portfolioId = Guid.Parse(portfolioDto.Id);
                var existingPortfolio = await _portfolioRepository.GetByIdAsync(portfolioId, cancellationToken);

                if (existingPortfolio == null)
                {
                    // Create new portfolio
                    var newPortfolio = new Portfolio(
                        portfolioId,
                        userId,
                        portfolioDto.Name,
                        portfolioDto.Currency)
                    {
                        Description = portfolioDto.Description,
                        CreatedAt = portfolioDto.CreatedAt,
                        UpdatedAt = portfolioDto.UpdatedAt
                    };

                    await _portfolioRepository.AddAsync(newPortfolio, cancellationToken);
                    result.PortfoliosImported++;
                }

                // Import positions for this portfolio
                var portfolioPositions = request.Positions
                    .Where(p => p.PortfolioId == portfolioDto.Id)
                    .ToList();

                foreach (var positionDto in portfolioPositions)
                {
                    var positionId = Guid.Parse(positionDto.Id);
                    var assetId = Guid.Parse(positionDto.AssetId);

                    // Ensure asset exists
                    var asset = await _assetRepository.GetByIdAsync(assetId, cancellationToken);
                    if (asset == null)
                    {
                        _logger.LogWarning("Asset {AssetId} not found during import, skipping position {PositionId}",
                            assetId, positionId);
                        continue;
                    }

                    var existingPosition = await _positionRepository.GetByIdAsync(positionId, cancellationToken);

                    if (existingPosition == null)
                    {
                        // Create new position
                        var newPosition = new Position(
                            positionId,
                            portfolioId,
                            assetId,
                            positionDto.Quantity,
                            Money.FromDecimal(positionDto.AverageCost, positionDto.Currency ?? "BRL"))
                        {
                            CreatedAt = positionDto.CreatedAt,
                            UpdatedAt = positionDto.UpdatedAt ?? positionDto.CreatedAt
                        };

                        await _positionRepository.AddAsync(newPosition, cancellationToken);
                        result.PositionsImported++;
                    }
                }
            }

            // 3. Import Recommendations
            foreach (var recommendationDto in request.Recommendations)
            {
                var recommendationId = Guid.Parse(recommendationDto.Id);
                var existingRecommendation = await _recommendationRepository.GetByIdAsync(recommendationId, cancellationToken);

                if (existingRecommendation == null)
                {
                    var portfolioId = Guid.Parse(recommendationDto.PortfolioId);
                    var assetId = recommendationDto.AssetId != null ? Guid.Parse(recommendationDto.AssetId) : (Guid?)null;

                    if (!Enum.TryParse<RecommendationActionType>(recommendationDto.ActionType, true, out var actionType))
                    {
                        _logger.LogWarning("Invalid action type {ActionType} for recommendation {RecommendationId}",
                            recommendationDto.ActionType, recommendationId);
                        continue;
                    }

                    if (!Enum.TryParse<Priority>(recommendationDto.Priority, true, out var priority))
                    {
                        priority = Priority.Medium;
                    }

                    if (!Enum.TryParse<RecommendationStatus>(recommendationDto.Status, true, out var status))
                    {
                        status = RecommendationStatus.Pending;
                    }

                    var newRecommendation = new Recommendation(
                        recommendationId,
                        portfolioId,
                        assetId,
                        actionType,
                        recommendationDto.SuggestedQuantity ?? 0,
                        Money.FromDecimal(recommendationDto.CurrentPrice ?? 0, recommendationDto.Currency ?? "BRL"),
                        recommendationDto.Justification ?? string.Empty)
                    {
                        TargetPrice = recommendationDto.TargetPrice.HasValue
                            ? Money.FromDecimal(recommendationDto.TargetPrice.Value, recommendationDto.Currency ?? "BRL")
                            : null,
                        DataSources = recommendationDto.DataSources,
                        Priority = priority,
                        Status = status,
                        ExpiresAt = recommendationDto.ExpiresAt,
                        CreatedAt = recommendationDto.CreatedAt,
                        ProcessedAt = recommendationDto.ProcessedAt
                    };

                    await _recommendationRepository.AddAsync(newRecommendation, cancellationToken);
                    result.RecommendationsImported++;
                }
            }

            // Commit transaction
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitTransactionAsync(cancellationToken);

            // Invalidate cache
            InvalidateAllCaches();

            result.Success = true;
            result.Message = "Data imported successfully";

            _logger.LogInformation(
                "Data imported for user {UserId}: {Portfolios} portfolios, {Positions} positions, {Recommendations} recommendations",
                userId, result.PortfoliosImported, result.PositionsImported, result.RecommendationsImported);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error importing data for user {UserId}", userId);

            await _unitOfWork.RollbackTransactionAsync(cancellationToken);

            result.Success = false;
            result.Message = $"Import failed: {ex.Message}";
        }

        return result;
    }

    private void InvalidateAllCaches()
    {
        if (_cache is MemoryCache memCache)
        {
            memCache.Compact(1.0); // Remove 100% of cache entries
        }
    }
}
