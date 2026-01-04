using MediatR;

namespace AAI.Application.UserProfile.Commands.DeleteAllData;

/// <summary>
/// Command to delete all user data (portfolios, positions, recommendations, profile)
/// This operation is irreversible
/// </summary>
public class DeleteAllDataCommand : IRequest<DeleteAllDataResult>
{
    public string UserId { get; set; } = string.Empty;

    /// <summary>
    /// Confirmation flag - must be true to proceed with deletion
    /// </summary>
    public bool ConfirmDeletion { get; set; }
}

/// <summary>
/// Result of delete all data operation
/// </summary>
public class DeleteAllDataResult
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public int PortfoliosDeleted { get; set; }
    public int PositionsDeleted { get; set; }
    public int RecommendationsDeleted { get; set; }
    public int AlertsDeleted { get; set; }
}
