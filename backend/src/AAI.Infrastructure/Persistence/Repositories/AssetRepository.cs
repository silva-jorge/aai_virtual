using AAI.Domain.Entities;
using AAI.Domain.Enums;
using AAI.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AAI.Infrastructure.Persistence.Repositories;

public class AssetRepository : Repository<Asset>, IAssetRepository
{
    public AssetRepository(AAIDbContext context) : base(context)
    {
    }

    public async Task<Asset?> GetByTickerAsync(string ticker, CancellationToken cancellationToken = default)
    {
        return await Context.Assets
            .FirstOrDefaultAsync(a => a.Ticker == ticker.ToUpperInvariant(), cancellationToken);
    }

    public async Task<IEnumerable<Asset>> GetActiveAssetsAsync(CancellationToken cancellationToken = default)
    {
        return await Context.Assets
            .Where(a => a.IsActive)
            .OrderBy(a => a.Ticker)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Asset>> GetByAssetClassAsync(AssetClass assetClass, CancellationToken cancellationToken = default)
    {
        return await Context.Assets
            .Where(a => a.AssetClass == assetClass && a.IsActive)
            .OrderBy(a => a.Ticker)
            .ToListAsync(cancellationToken);
    }
}
