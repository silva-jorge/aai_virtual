using Serilog;
using Serilog.Context;

namespace AAI.WebAPI.Middleware;

/// <summary>
/// Middleware para logging de requisições HTTP
/// </summary>
public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestLoggingMiddleware> _logger;

    public RequestLoggingMiddleware(
        RequestDelegate next,
        ILogger<RequestLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var requestId = Guid.NewGuid().ToString();
        
        using (LogContext.PushProperty("RequestId", requestId))
        {
            context.Response.Headers.Add("X-Request-Id", requestId);

            var startTime = DateTime.UtcNow;
            
            _logger.LogInformation(
                "Requisição iniciada: {Method} {Path}",
                context.Request.Method,
                context.Request.Path
            );

            try
            {
                await _next(context);

                var elapsedMs = (DateTime.UtcNow - startTime).TotalMilliseconds;
                
                _logger.LogInformation(
                    "Requisição concluída: {Method} {Path} {StatusCode} em {ElapsedMs}ms",
                    context.Request.Method,
                    context.Request.Path,
                    context.Response.StatusCode,
                    elapsedMs
                );
            }
            catch (Exception ex)
            {
                var elapsedMs = (DateTime.UtcNow - startTime).TotalMilliseconds;
                
                _logger.LogError(
                    ex,
                    "Requisição falhou: {Method} {Path} em {ElapsedMs}ms",
                    context.Request.Method,
                    context.Request.Path,
                    elapsedMs
                );
                
                throw;
            }
        }
    }
}

/// <summary>
/// Extension method para registrar o middleware
/// </summary>
public static class RequestLoggingMiddlewareExtensions
{
    public static IApplicationBuilder UseRequestLoggingMiddleware(
        this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<RequestLoggingMiddleware>();
    }
}
