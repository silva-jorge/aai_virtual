using AAI.Domain.Interfaces;
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
