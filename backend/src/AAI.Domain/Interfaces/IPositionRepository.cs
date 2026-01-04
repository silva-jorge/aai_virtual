using AAI.Domain.Entities;

namespace AAI.Domain.Interfaces;

/// <summary>
/// Repository interface for Position entity persistence operations
/// </summary>
public interface IPositionRepository : IRepository<Position>
{
    /// <summary>
    /// Get all positions for a specific portfolio
    /// </summary>
    /// <param name="portfolioId">The portfolio ID</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of positions in the portfolio</returns>
    Task<IEnumerable<Position>> GetByPortfolioIdAsync(
        string portfolioId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Get positions for a specific asset across all portfolios
    /// </summary>
    /// <param name="assetId">The asset ID</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of positions holding this asset</returns>
    Task<IEnumerable<Position>> GetByAssetIdAsync(
        string assetId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Get a specific position by ID with related entities eagerly loaded
    /// </summary>
    /// <param name="id">The position ID</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Position with Asset and Portfolio data loaded</returns>
    Task<Position?> GetWithDetailsAsync(
        string id,
        CancellationToken cancellationToken = default);
}
