using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using System.Text.Json;
using FluentValidation;

namespace AAI.WebAPI.Middleware;

/// <summary>
/// Middleware global para tratamento de exceções
/// </summary>
public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(
        RequestDelegate next,
        ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exceção não tratada durante o processamento da requisição");
            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        var (statusCode, message) = exception switch
        {
            ValidationException validationEx => (
                HttpStatusCode.BadRequest,
                string.Join("; ", validationEx.Errors.Select(e => e.ErrorMessage))
            ),
            KeyNotFoundException => (
                HttpStatusCode.NotFound,
                "Recurso não encontrado"
            ),
            UnauthorizedAccessException => (
                HttpStatusCode.Unauthorized,
                "Acesso não autorizado"
            ),
            InvalidOperationException => (
                HttpStatusCode.BadRequest,
                exception.Message
            ),
            ArgumentException => (
                HttpStatusCode.BadRequest,
                exception.Message
            ),
            _ => (
                HttpStatusCode.InternalServerError,
                "Ocorreu um erro interno no servidor"
            )
        };

        context.Response.StatusCode = (int)statusCode;

        var response = new
        {
            statusCode = (int)statusCode,
            message,
            timestamp = DateTime.UtcNow,
#if DEBUG
            detail = exception.ToString()
#endif
        };

        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        await context.Response.WriteAsync(
            JsonSerializer.Serialize(response, options)
        );
    }
}

/// <summary>
/// Extension method para registrar o middleware
/// </summary>
public static class ExceptionHandlingMiddlewareExtensions
{
    public static IApplicationBuilder UseExceptionHandlingMiddleware(
        this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ExceptionHandlingMiddleware>();
    }
}
