using AAI.Application.Portfolio.DTOs;
using MediatR;

namespace AAI.Application.Portfolio.Queries.GetPerformanceMetrics;

public record GetPerformanceMetricsQuery(string UserId) : IRequest<PerformanceMetricsDto>;
