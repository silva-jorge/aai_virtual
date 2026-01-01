using AAI.Domain.Entities;
using AAI.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AAI.Infrastructure.Persistence.Repositories;

public class PortfolioRepository : Repository<Portfolio>, IPortfolioRepository
{
    public PortfolioRepository(AAIDbContext context) : base(context)
    {
    }

    public async Task<Portfolio?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await Context.Portfolios
            .FirstOrDefaultAsync(p => p.UserId == userId, cancellationToken);
    }

    public async Task<Portfolio?> GetByIdWithPositionsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await Context.Portfolios
            .Include(p => p.Positions)
                .ThenInclude(pos => pos.Asset)
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }
}
