using AAI.Application.Portfolio.DTOs;
using AAI.Application.Rebalancing.DTOs;
using AAI.Application.UserProfile.DTOs;

namespace AAI.Application.UserProfile.Queries.ExportData;

/// <summary>
/// Response containing all user data for export
/// </summary>
public class ExportDataResponse
{
    public UserProfileDto? UserProfile { get; set; }
    public List<PortfolioSummaryDto> Portfolios { get; set; } = new();
    public List<PositionDto> Positions { get; set; } = new();
    public List<RecommendationDto> Recommendations { get; set; } = new();
    public DateTime ExportedAt { get; set; }
}
