namespace AAI.Domain.Interfaces;

/// <summary>
/// Repository interface for Recommendation entity
/// </summary>
public interface IRecommendationRepository : IRepository<Entities.Recommendation>
{
    /// <summary>
    /// Gets active recommendations for a portfolio
    /// </summary>
    Task<IEnumerable<Entities.Recommendation>> GetActiveByPortfolioIdAsync(
        Guid portfolioId, 
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets all recommendations for a portfolio (including expired/rejected)
    /// </summary>
    Task<IEnumerable<Entities.Recommendation>> GetAllByPortfolioIdAsync(
        Guid portfolioId, 
        CancellationToken cancellationToken = default);
}
