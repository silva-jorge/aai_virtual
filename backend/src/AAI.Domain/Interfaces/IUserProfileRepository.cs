namespace AAI.Domain.Interfaces;

/// <summary>
/// Repository interface for UserProfile entity.
/// </summary>
public interface IUserProfileRepository : IRepository<Entities.UserProfile>
{
    /// <summary>
    /// Finds a user profile by ID including the portfolio.
    /// </summary>
    Task<Entities.UserProfile?> GetByIdWithPortfolioAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Checks if any user profile exists in the system.
    /// </summary>
    Task<bool> AnyUserExistsAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets the first (and should be only) user profile.
    /// This is a single-user application.
    /// </summary>
    Task<Entities.UserProfile?> GetFirstUserAsync(CancellationToken cancellationToken = default);
}
