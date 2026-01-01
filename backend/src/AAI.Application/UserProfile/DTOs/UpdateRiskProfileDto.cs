namespace AAI.Application.UserProfile.DTOs;

public class UpdateRiskProfileDto
{
    public string RiskProfile { get; set; } = string.Empty;
    public string? InvestmentGoal { get; set; }
    public decimal VolatilityTolerance { get; set; }
    public int TimeHorizonMonths { get; set; }
}
