using AAI.Application.Portfolio.DTOs;
using MediatR;

namespace AAI.Application.Portfolio.Queries.GetAllocationBreakdown;

public record GetAllocationBreakdownQuery(string UserId) : IRequest<AllocationBreakdownDto>;
