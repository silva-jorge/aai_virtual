using MediatR;

namespace AAI.Application.UserProfile.Queries.ExportData;

/// <summary>
/// Query to export all user data from the system
/// </summary>
public record ExportDataQuery(string UserId) : IRequest<ExportDataResponse>
{
    public string UserId { get; init; } = UserId;
}
