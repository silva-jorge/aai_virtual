using AAI.Domain.Entities;
using AAI.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AAI.Infrastructure.Persistence.Repositories;

public class UserProfileRepository : Repository<UserProfile>, IUserProfileRepository
{
    public UserProfileRepository(AAIDbContext context) : base(context)
    {
    }

    public async Task<UserProfile?> GetByIdWithPortfolioAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await Context.UserProfiles
            .Include(u => u.Portfolio)
            .FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
    }

    public async Task<bool> AnyUserExistsAsync(CancellationToken cancellationToken = default)
    {
        return await Context.UserProfiles.AnyAsync(cancellationToken);
    }

    public async Task<UserProfile?> GetFirstUserAsync(CancellationToken cancellationToken = default)
    {
        return await Context.UserProfiles
            .Include(u => u.Portfolio)
            .FirstOrDefaultAsync(cancellationToken);
    }
}
