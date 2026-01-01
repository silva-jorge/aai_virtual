using AAI.Domain.Entities;
using AAI.Domain.Enums;
using AAI.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AAI.Infrastructure.Persistence.Repositories;

public class RecommendationRepository : Repository<Recommendation>, IRecommendationRepository
{
    public RecommendationRepository(AAIDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Recommendation>> GetActiveByPortfolioIdAsync(
        Guid portfolioId, 
        CancellationToken cancellationToken = default)
    {
        return await Context.Set<Recommendation>()
            .Where(r => r.PortfolioId == portfolioId 
                && r.Status == RecommendationStatus.Pendente
                && (r.ExpiresAt == null || r.ExpiresAt > DateTime.UtcNow))
            .OrderByDescending(r => r.Priority)
            .ThenByDescending(r => r.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Recommendation>> GetAllByPortfolioIdAsync(
        Guid portfolioId, 
        CancellationToken cancellationToken = default)
    {
        return await Context.Set<Recommendation>()
            .Where(r => r.PortfolioId == portfolioId)
            .OrderByDescending(r => r.CreatedAt)
            .ToListAsync(cancellationToken);
    }
}
