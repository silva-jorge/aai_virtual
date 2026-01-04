using Microsoft.EntityFrameworkCore;
using AAI.Domain.Entities;
using AAI.Domain.Interfaces;

namespace AAI.Infrastructure.Persistence.Repositories;

/// <summary>
/// Repository implementation for Position entity persistence
/// Handles all database operations for positions
/// </summary>
public class PositionRepository : Repository<Position>, IPositionRepository
{
    public PositionRepository(AAIDbContext context) : base(context)
    {
    }

    /// <summary>
    /// Get all positions for a specific portfolio with related data
    /// </summary>
    public async Task<IEnumerable<Position>> GetByPortfolioIdAsync(
        string portfolioId,
        CancellationToken cancellationToken = default)
    {
        return await _context.Positions
            .AsNoTracking()
            .Where(p => p.PortfolioId == portfolioId)
            .Include(p => p.Asset)
            .OrderByDescending(p => p.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    /// <summary>
    /// Get all positions holding a specific asset across portfolios
    /// </summary>
    public async Task<IEnumerable<Position>> GetByAssetIdAsync(
        string assetId,
        CancellationToken cancellationToken = default)
    {
        return await _context.Positions
            .AsNoTracking()
            .Where(p => p.AssetId == assetId)
            .Include(p => p.Portfolio)
            .OrderByDescending(p => p.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    /// <summary>
    /// Get a position with all related entities fully loaded
    /// </summary>
    public async Task<Position?> GetWithDetailsAsync(
        string id,
        CancellationToken cancellationToken = default)
    {
        return await _context.Positions
            .AsNoTracking()
            .Where(p => p.Id == id)
            .Include(p => p.Asset)
            .Include(p => p.Portfolio)
            .FirstOrDefaultAsync(cancellationToken);
    }
}
