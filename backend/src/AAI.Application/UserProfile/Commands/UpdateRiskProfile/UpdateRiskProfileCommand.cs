using AAI.Application.UserProfile.DTOs;
using MediatR;

namespace AAI.Application.UserProfile.Commands.UpdateRiskProfile;

public class UpdateRiskProfileCommand : IRequest<UserProfileDto>
{
    public string UserId { get; set; } = string.Empty;
    public string RiskProfile { get; set; } = string.Empty;
    public string? InvestmentGoal { get; set; }
    public decimal VolatilityTolerance { get; set; }
    public int TimeHorizonMonths { get; set; }
}
