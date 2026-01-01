using AAI.Application.Portfolio.DTOs;
using MediatR;

namespace AAI.Application.Portfolio.Queries.GetPortfolioSummary;

public record GetPortfolioSummaryQuery(string UserId) : IRequest<PortfolioSummaryDto>;
