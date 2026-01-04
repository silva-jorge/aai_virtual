using AAI.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace AAI.Application.UserProfile.Commands.DeleteAllData;

/// <summary>
/// Handler for DeleteAllDataCommand - deletes all user data
/// </summary>
public class DeleteAllDataCommandHandler : IRequestHandler<DeleteAllDataCommand, DeleteAllDataResult>
{
    private readonly IUserProfileRepository _userProfileRepository;
    private readonly IPortfolioRepository _portfolioRepository;
    private readonly IPositionRepository _positionRepository;
    private readonly IRecommendationRepository _recommendationRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMemoryCache _cache;
    private readonly ILogger<DeleteAllDataCommandHandler> _logger;

    public DeleteAllDataCommandHandler(
        IUserProfileRepository userProfileRepository,
        IPortfolioRepository portfolioRepository,
        IPositionRepository positionRepository,
        IRecommendationRepository recommendationRepository,
        IUnitOfWork unitOfWork,
        IMemoryCache cache,
        ILogger<DeleteAllDataCommandHandler> logger)
    {
        _userProfileRepository = userProfileRepository;
        _portfolioRepository = portfolioRepository;
        _positionRepository = positionRepository;
        _recommendationRepository = recommendationRepository;
        _unitOfWork = unitOfWork;
        _cache = cache;
        _logger = logger;
    }

    public async Task<DeleteAllDataResult> Handle(DeleteAllDataCommand request, CancellationToken cancellationToken)
    {
        var result = new DeleteAllDataResult();

        if (!request.ConfirmDeletion)
        {
            result.Success = false;
            result.Message = "Deletion not confirmed. Set ConfirmDeletion to true to proceed.";
            return result;
        }

        var userId = Guid.Parse(request.UserId);

        try
        {
            // Start transaction
            await _unitOfWork.BeginTransactionAsync(cancellationToken);

            _logger.LogWarning("Starting deletion of all data for user {UserId}", userId);

            // 1. Get user's portfolio
            var portfolio = await _portfolioRepository.GetByUserIdAsync(userId, cancellationToken);

            if (portfolio != null)
            {
                // 2. Delete all recommendations for this portfolio
                var recommendations = await _recommendationRepository.GetAllByPortfolioIdAsync(
                    portfolio.Id, cancellationToken);

                foreach (var recommendation in recommendations)
                {
                    await _recommendationRepository.DeleteAsync(recommendation, cancellationToken);
                    result.RecommendationsDeleted++;
                }

                // 3. Delete all positions (transactions will cascade delete via EF Core configuration)
                var positions = await _positionRepository.GetAllByPortfolioIdAsync(
                    portfolio.Id, cancellationToken);

                foreach (var position in positions)
                {
                    await _positionRepository.DeleteAsync(position, cancellationToken);
                    result.PositionsDeleted++;
                }

                // 4. Delete portfolio
                await _portfolioRepository.DeleteAsync(portfolio, cancellationToken);
                result.PortfoliosDeleted++;
            }

            // 5. Delete user profile (password and settings)
            var userProfile = await _userProfileRepository.GetByIdAsync(userId, cancellationToken);
            if (userProfile != null)
            {
                await _userProfileRepository.DeleteAsync(userProfile, cancellationToken);
            }

            // Commit transaction
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitTransactionAsync(cancellationToken);

            // Invalidate all caches
            InvalidateAllCaches();

            result.Success = true;
            result.Message = "All user data deleted successfully";

            _logger.LogWarning(
                "Deleted all data for user {UserId}: {Portfolios} portfolios, {Positions} positions, {Recommendations} recommendations, {Alerts} alerts",
                userId, result.PortfoliosDeleted, result.PositionsDeleted, result.RecommendationsDeleted, result.AlertsDeleted);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting all data for user {UserId}", userId);

            await _unitOfWork.RollbackTransactionAsync(cancellationToken);

            result.Success = false;
            result.Message = $"Deletion failed: {ex.Message}";
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
