using AAI.Application.Rebalancing.Commands.RequestRecommendations;
using AAI.Application.Rebalancing.DTOs;
using AAI.Application.Rebalancing.Queries.GetRecommendations;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AAI.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class RebalancingController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<RebalancingController> _logger;

    public RebalancingController(IMediator mediator, ILogger<RebalancingController> logger)
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
    /// Get active recommendations
    /// </summary>
    [HttpGet("recommendations")]
    public async Task<ActionResult<IEnumerable<RecommendationDto>>> GetRecommendations()
    {
        try
        {
            var userId = GetUserId();
            var query = new GetRecommendationsQuery(userId);
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
            _logger.LogError(ex, "Error getting recommendations");
            return StatusCode(500, new { message = "Error retrieving recommendations" });
        }
    }

    /// <summary>
    /// Request new recommendations from AI
    /// </summary>
    [HttpPost("recommendations")]
    public async Task<ActionResult<IEnumerable<RecommendationDto>>> RequestRecommendations(
        [FromBody] RequestRecommendationsDto dto)
    {
        try
        {
            var userId = GetUserId();
            var command = new RequestRecommendationsCommand
            {
                UserId = userId,
                ForceRegenerate = dto.ForceRegenerate
            };
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogWarning(ex, "Portfolio not found");
            return NotFound(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error requesting recommendations");
            return StatusCode(500, new { message = "Error generating recommendations" });
        }
    }
}
