namespace AAI.Domain.Interfaces;

/// <summary>
/// Repository interface for Portfolio entity.
/// </summary>
public interface IPortfolioRepository : IRepository<Entities.Portfolio>
{
    /// <summary>
    /// Gets a portfolio by user ID.
    /// </summary>
    Task<Entities.Portfolio?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a portfolio with all positions and assets.
    /// </summary>
    Task<Entities.Portfolio?> GetByIdWithPositionsAsync(Guid id, CancellationToken cancellationToken = default);
}
