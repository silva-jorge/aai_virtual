using AAI.Application.Portfolio.DTOs;
using AAI.Application.Portfolio.Queries.GetAllocationBreakdown;
using AAI.Application.Portfolio.Queries.GetPerformanceMetrics;
using AAI.Application.Portfolio.Queries.GetPortfolioSummary;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AAI.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PortfolioController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<PortfolioController> _logger;

    public PortfolioController(IMediator mediator, ILogger<PortfolioController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    private string GetUserId()
    {
        return User.FindFirst(ClaimTypes.NameIdentifier)?.Value
            ?? throw new UnauthorizedAccessException("User ID not found in token");
    }

    /// <summary>
    /// Get portfolio summary
    /// </summary>
    [HttpGet("summary")]
    public async Task<ActionResult<PortfolioSummaryDto>> GetSummary()
    {
        try
        {
            var userId = GetUserId();
            var query = new GetPortfolioSummaryQuery(userId);
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogWarning(ex, "Portfolio not found");
            return NotFound(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting portfolio summary");
            return StatusCode(500, new { message = "Error retrieving portfolio summary" });
        }
    }

    /// <summary>
    /// Get allocation breakdown
    /// </summary>
    [HttpGet("allocation")]
    public async Task<ActionResult<AllocationBreakdownDto>> GetAllocation()
    {
        try
        {
            var userId = GetUserId();
            var query = new GetAllocationBreakdownQuery(userId);
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogWarning(ex, "Portfolio not found");
            return NotFound(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting allocation breakdown");
            return StatusCode(500, new { message = "Error retrieving allocation breakdown" });
        }
    }

    /// <summary>
    /// Get performance metrics
    /// </summary>
    [HttpGet("performance")]
    public async Task<ActionResult<PerformanceMetricsDto>> GetPerformance()
    {
        try
        {
            var userId = GetUserId();
            var query = new GetPerformanceMetricsQuery(userId);
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogWarning(ex, "Portfolio not found");
            return NotFound(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting performance metrics");
            return StatusCode(500, new { message = "Error retrieving performance metrics" });
        }
    }
}
