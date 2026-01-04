using AAI.Domain.Entities;

namespace AAI.Domain.Interfaces;

/// <summary>
/// Interface para Unit of Work pattern
/// </summary>
public interface IUnitOfWork : IDisposable
{
    IRepository<UserProfile> UserProfiles { get; }
    IRepository<Portfolio> Portfolios { get; }
    IRepository<Position> Positions { get; }
    IRepository<Asset> Assets { get; }
    IRepository<Transaction> Transactions { get; }
    IRepository<Recommendation> Recommendations { get; }
    IRepository<Alert> Alerts { get; }
    IRepository<AlertHistory> AlertHistories { get; }
    IRepository<Benchmark> Benchmarks { get; }
    IRepository<BenchmarkValue> BenchmarkValues { get; }
    IRepository<MarketEvent> MarketEvents { get; }
    IRepository<NewsItem> NewsItems { get; }
    IRepository<PriceHistory> PriceHistories { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    Task BeginTransactionAsync(CancellationToken cancellationToken = default);
    Task CommitTransactionAsync(CancellationToken cancellationToken = default);
    Task RollbackTransactionAsync(CancellationToken cancellationToken = default);
}
