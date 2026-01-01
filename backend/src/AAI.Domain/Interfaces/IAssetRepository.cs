namespace AAI.Domain.Interfaces;

/// <summary>
/// Repository interface for Asset entity.
/// </summary>
public interface IAssetRepository : IRepository<Entities.Asset>
{
    /// <summary>
    /// Finds an asset by ticker symbol.
    /// </summary>
    Task<Entities.Asset?> GetByTickerAsync(string ticker, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets all active assets.
    /// </summary>
    Task<IEnumerable<Entities.Asset>> GetActiveAssetsAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets assets by asset class.
    /// </summary>
    Task<IEnumerable<Entities.Asset>> GetByAssetClassAsync(Enums.AssetClass assetClass, CancellationToken cancellationToken = default);
}
