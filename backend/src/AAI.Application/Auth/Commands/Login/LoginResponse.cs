namespace AAI.Application.Auth.Commands.Login;

public record LoginResponse
{
    public Guid UserId { get; init; }
    public string Token { get; init; } = string.Empty;
    public bool Success { get; init; }
    public string Message { get; init; } = string.Empty;
    public UserProfileDto? UserProfile { get; init; }
}

public record UserProfileDto
{
    public Guid Id { get; init; }
    public string RiskProfile { get; init; } = string.Empty;
    public string? InvestmentGoal { get; init; }
    public decimal VolatilityTolerance { get; init; }
    public int TimeHorizonMonths { get; init; }
    public decimal RebalanceThresholdPercent { get; init; }
    public string TargetAllocationJson { get; init; } = string.Empty;
}
