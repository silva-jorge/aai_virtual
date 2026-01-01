namespace AAI.Domain.Common.Interfaces;

/// <summary>
/// Interface para serviço de dados de mercado
/// </summary>
public interface IMarketDataService
{
    Task<decimal?> GetCurrentPriceAsync(string ticker, CancellationToken cancellationToken = default);
    Task<Dictionary<string, decimal>> GetMultiplePricesAsync(IEnumerable<string> tickers, CancellationToken cancellationToken = default);
    Task<IEnumerable<(DateTime Date, decimal Price)>> GetPriceHistoryAsync(string ticker, DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default);
    Task<bool> IsMarketOpenAsync(CancellationToken cancellationToken = default);
}
