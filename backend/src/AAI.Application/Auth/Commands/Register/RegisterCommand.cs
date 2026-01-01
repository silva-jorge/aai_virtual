using MediatR;

namespace AAI.Application.Auth.Commands.Register;

public record RegisterCommand : IRequest<RegisterResponse>
{
    public string Pin { get; init; } = string.Empty;
    public string RiskProfile { get; init; } = "moderado";
    public string? InvestmentGoal { get; init; }
    public decimal VolatilityTolerance { get; init; } = 50m;
    public int TimeHorizonMonths { get; init; } = 60;
    public decimal RebalanceThresholdPercent { get; init; } = 5m;
    public string TargetAllocationJson { get; init; } = "{}";
}
