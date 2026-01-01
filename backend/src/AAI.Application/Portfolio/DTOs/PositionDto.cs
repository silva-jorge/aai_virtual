namespace AAI.Application.Portfolio.DTOs;

/// <summary>
/// DTO para uma posição individual no portfólio
/// </summary>
public class PositionDto
{
    public string Id { get; set; } = string.Empty;
    public string AssetId { get; set; } = string.Empty;
    public string Ticker { get; set; } = string.Empty;
    public string AssetName { get; set; } = string.Empty;
    public string AssetClass { get; set; } = string.Empty;
    public decimal Quantity { get; set; }
    public decimal AverageCost { get; set; }
    public decimal CurrentPrice { get; set; }
    public decimal TotalInvested { get; set; }
    public decimal CurrentValue { get; set; }
    public decimal AllocationPercent { get; set; }
    public decimal GainLoss { get; set; }
    public decimal GainLossPercent { get; set; }
}
