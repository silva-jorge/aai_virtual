namespace AAI.Application.UserProfile.DTOs;

public class UserProfileDto
{
    public string Id { get; set; } = string.Empty;
    public string RiskProfile { get; set; } = string.Empty;
    public string? InvestmentGoal { get; set; }
    public decimal VolatilityTolerance { get; set; }
    public int TimeHorizonMonths { get; set; }
    public decimal RebalanceThresholdPercent { get; set; }
    public string TargetAllocationJson { get; set; } = "{}";
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
