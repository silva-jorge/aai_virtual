namespace AAI.Application.Portfolio.DTOs;

/// <summary>
/// DTO para métricas de performance do portfólio
/// </summary>
public class PerformanceMetricsDto
{
    public decimal TotalReturn { get; set; }
    public decimal TotalReturnPercent { get; set; }
    public decimal DayChange { get; set; }
    public decimal DayChangePercent { get; set; }
    public decimal MonthReturn { get; set; }
    public decimal MonthReturnPercent { get; set; }
    public decimal YearReturn { get; set; }
    public decimal YearReturnPercent { get; set; }
    public List<PerformanceHistoryDto> History { get; set; } = new();
}

/// <summary>
/// DTO para histórico de performance
/// </summary>
public class PerformanceHistoryDto
{
    public DateTime Date { get; set; }
    public decimal Value { get; set; }
    public decimal ReturnPercent { get; set; }
}
