using AAI.Application.UserProfile.DTOs;
using MediatR;

namespace AAI.Application.UserProfile.Commands.UpdateThresholds;

public class UpdateThresholdsCommand : IRequest<UserProfileDto>
{
    public string UserId { get; set; } = string.Empty;
    public decimal RebalanceThresholdPercent { get; set; }
    public string TargetAllocationJson { get; set; } = "{}";
}
