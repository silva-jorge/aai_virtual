using AAI.Application.Portfolio.DTOs;
using AAI.Application.Rebalancing.DTOs;
using AAI.Application.UserProfile.DTOs;
using MediatR;

namespace AAI.Application.UserProfile.Commands.ImportData;

/// <summary>
/// Command to import user data from a backup
/// </summary>
public class ImportDataCommand : IRequest<ImportDataResult>
{
    public string UserId { get; set; } = string.Empty;
    public UserProfileDto? UserProfile { get; set; }
    public List<PortfolioSummaryDto> Portfolios { get; set; } = new();
    public List<PositionDto> Positions { get; set; } = new();
    public List<RecommendationDto> Recommendations { get; set; } = new();
}

/// <summary>
/// Result of import operation
/// </summary>
public class ImportDataResult
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public int PortfoliosImported { get; set; }
    public int PositionsImported { get; set; }
    public int RecommendationsImported { get; set; }
}
