using AAI.Domain.Entities;
using AAI.Domain.Interfaces;
using AAI.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace AAI.Infrastructure.Persistence;

/// <summary>
/// Implementação do padrão Unit of Work
/// </summary>
public class UnitOfWork : IUnitOfWork
{
    private readonly AAIDbContext _context;
    private IDbContextTransaction? _transaction;

    public UnitOfWork(AAIDbContext context)
    {
        _context = context;
    }

    public IRepository<UserProfile> UserProfiles => new Repository<UserProfile>(_context);
    public IRepository<Portfolio> Portfolios => new Repository<Portfolio>(_context);
    public IRepository<Position> Positions => new Repository<Position>(_context);
    public IRepository<Asset> Assets => new Repository<Asset>(_context);
    public IRepository<Transaction> Transactions => new Repository<Transaction>(_context);
    public IRepository<Recommendation> Recommendations => new Repository<Recommendation>(_context);
    public IRepository<Alert> Alerts => new Repository<Alert>(_context);
    public IRepository<AlertHistory> AlertHistories => new Repository<AlertHistory>(_context);
    public IRepository<Benchmark> Benchmarks => new Repository<Benchmark>(_context);
    public IRepository<BenchmarkValue> BenchmarkValues => new Repository<BenchmarkValue>(_context);
    public IRepository<MarketEvent> MarketEvents => new Repository<MarketEvent>(_context);
    public IRepository<NewsItem> NewsItems => new Repository<NewsItem>(_context);
    public IRepository<PriceHistory> PriceHistories => new Repository<PriceHistory>(_context);

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        _transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
    }

    public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            await SaveChangesAsync(cancellationToken);
            await (_transaction?.CommitAsync(cancellationToken) ?? Task.CompletedTask);
        }
        catch
        {
            await RollbackTransactionAsync(cancellationToken);
            throw;
        }
        finally
        {
            _transaction?.Dispose();
            _transaction = null;
        }
    }

    public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
    {
        await (_transaction?.RollbackAsync(cancellationToken) ?? Task.CompletedTask);
        _transaction?.Dispose();
        _transaction = null;
    }

    public void Dispose()
    {
        _transaction?.Dispose();
        _context.Dispose();
    }
}
