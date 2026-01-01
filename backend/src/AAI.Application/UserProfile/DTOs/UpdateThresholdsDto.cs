namespace AAI.Application.UserProfile.DTOs;

public class UpdateThresholdsDto
{
    public decimal RebalanceThresholdPercent { get; set; }
    public string TargetAllocationJson { get; set; } = "{}";
}
