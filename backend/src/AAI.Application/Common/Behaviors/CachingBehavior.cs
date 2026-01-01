using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace AAI.Application.Common.Behaviors;

/// <summary>
/// Behavior do MediatR para caching de queries
/// </summary>
public class CachingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IMemoryCache _cache;
    private readonly ILogger<CachingBehavior<TRequest, TResponse>> _logger;

    public CachingBehavior(
        IMemoryCache cache,
        ILogger<CachingBehavior<TRequest, TResponse>> logger)
    {
        _cache = cache;
        _logger = logger;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        // Apenas queries devem ser cacheadas (convenção: queries terminam com "Query")
        var requestName = typeof(TRequest).Name;
        if (!requestName.EndsWith("Query", StringComparison.OrdinalIgnoreCase))
        {
            return await next();
        }

        var cacheKey = GenerateCacheKey(request);

        if (_cache.TryGetValue(cacheKey, out TResponse cachedResponse) && cachedResponse != null)
        {
            _logger.LogDebug("Cache hit for {RequestName}", requestName);
            return cachedResponse;
        }

        _logger.LogDebug("Cache miss for {RequestName}", requestName);
        var response = await next();

        var cacheOptions = new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(15),
            SlidingExpiration = TimeSpan.FromMinutes(5)
        };

        _cache.Set(cacheKey, response, cacheOptions);

        return response;
    }

    private static string GenerateCacheKey(TRequest request)
    {
        var requestName = typeof(TRequest).Name;
        var requestJson = JsonSerializer.Serialize(request);
        var hash = Convert.ToBase64String(
            System.Security.Cryptography.SHA256.HashData(
                System.Text.Encoding.UTF8.GetBytes(requestJson)));

        return $"{requestName}:{hash}";
    }
}
