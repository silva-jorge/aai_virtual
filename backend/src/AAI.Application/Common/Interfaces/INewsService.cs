namespace AAI.Application.Common.Interfaces;

/// <summary>
/// Interface para serviço de notícias
/// </summary>
public interface INewsService
{
    Task<IEnumerable<NewsItemDto>> GetLatestNewsAsync(int count = 20, CancellationToken cancellationToken = default);
    Task<IEnumerable<NewsItemDto>> GetNewsForTickerAsync(string ticker, CancellationToken cancellationToken = default);
    Task<string> SummarizeNewsAsync(string newsContent, CancellationToken cancellationToken = default);
}

public record NewsItemDto(
    string Title,
    string Source,
    DateTime PublishedAt,
    string Content,
    string? Summary,
    string? Url
);
