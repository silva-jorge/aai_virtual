using AAI.Application.UserProfile.Commands.DeleteAllData;
using AAI.Application.UserProfile.Commands.ImportData;
using AAI.Application.UserProfile.Commands.UpdateRiskProfile;
using AAI.Application.UserProfile.Commands.UpdateThresholds;
using AAI.Application.UserProfile.DTOs;
using AAI.Application.UserProfile.Queries.ExportData;
using AAI.Application.UserProfile.Queries.GetUserProfile;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AAI.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ProfileController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<ProfileController> _logger;

    public ProfileController(IMediator mediator, ILogger<ProfileController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    private string GetUserId()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
        {
            throw new UnauthorizedAccessException("User ID not found in token");
        }
        return userId;
    }

    /// <summary>
    /// Get user profile
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<UserProfileDto>> GetProfile()
    {
        try
        {
            var userId = GetUserId();
            var query = new GetUserProfileQuery(userId);
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogWarning(ex, "Profile not found");
            return NotFound(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting profile");
            return StatusCode(500, new { message = "Error retrieving profile" });
        }
    }

    /// <summary>
    /// Update risk profile settings
    /// </summary>
    [HttpPut("risk-profile")]
    public async Task<ActionResult<UserProfileDto>> UpdateRiskProfile([FromBody] UpdateRiskProfileDto dto)
    {
        try
        {
            var userId = GetUserId();
            var command = new UpdateRiskProfileCommand
            {
                UserId = userId,
                RiskProfile = dto.RiskProfile,
                InvestmentGoal = dto.InvestmentGoal,
                VolatilityTolerance = dto.VolatilityTolerance,
                TimeHorizonMonths = dto.TimeHorizonMonths
            };

            var result = await _mediator.Send(command);
            return Ok(result);
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogWarning(ex, "Profile not found");
            return NotFound(new { message = ex.Message });
        }
        catch (ArgumentException ex)
        {
            _logger.LogWarning(ex, "Invalid argument");
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating risk profile");
            return StatusCode(500, new { message = "Error updating risk profile" });
        }
    }

    /// <summary>
    /// Update rebalance thresholds
    /// </summary>
    [HttpPut("thresholds")]
    public async Task<ActionResult<UserProfileDto>> UpdateThresholds([FromBody] UpdateThresholdsDto dto)
    {
        try
        {
            var userId = GetUserId();
            var command = new UpdateThresholdsCommand
            {
                UserId = userId,
                RebalanceThresholdPercent = dto.RebalanceThresholdPercent,
                TargetAllocationJson = dto.TargetAllocationJson
            };

            var result = await _mediator.Send(command);
            return Ok(result);
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogWarning(ex, "Profile not found");
            return NotFound(new { message = ex.Message });
        }
        catch (ArgumentException ex)
        {
            _logger.LogWarning(ex, "Invalid argument");
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating thresholds");
            return StatusCode(500, new { message = "Error updating thresholds" });
        }
    }

    /// <summary>
    /// Export user data (profile, portfolios, positions, recommendations)
    /// </summary>
    [HttpGet("export")]
    public async Task<ActionResult<ExportDataResponse>> ExportData()
    {
        try
        {
            var userId = GetUserId();
            var query = new ExportDataQuery(userId);
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogWarning(ex, "Profile not found");
            return NotFound(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error exporting data");
            return StatusCode(500, new { message = "Error exporting data" });
        }
    }

    /// <summary>
    /// Import user data from backup (profile, portfolios, positions, recommendations)
    /// </summary>
    [HttpPost("import")]
    public async Task<ActionResult<ImportDataResult>> ImportData([FromBody] ImportDataCommand command)
    {
        try
        {
            var userId = GetUserId();
            command.UserId = userId; // Ensure the user can only import their own data

            var result = await _mediator.Send(command);

            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
        catch (ArgumentException ex)
        {
            _logger.LogWarning(ex, "Invalid import data");
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error importing data");
            return StatusCode(500, new { message = "Error importing data" });
        }
    }

    /// <summary>
    /// Delete all user data (irreversible operation - requires confirmation)
    /// </summary>
    [HttpDelete("all-data")]
    public async Task<ActionResult<DeleteAllDataResult>> DeleteAllData([FromBody] DeleteAllDataCommand command)
    {
        try
        {
            var userId = GetUserId();
            command.UserId = userId; // Ensure the user can only delete their own data

            if (!command.ConfirmDeletion)
            {
                return BadRequest(new
                {
                    message = "Deletion not confirmed. This operation is irreversible. Set ConfirmDeletion to true to proceed."
                });
            }

            var result = await _mediator.Send(command);

            if (result.Success)
            {
                _logger.LogWarning("User {UserId} deleted all their data", userId);
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
        catch (ArgumentException ex)
        {
            _logger.LogWarning(ex, "Invalid delete request");
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting all data");
            return StatusCode(500, new { message = "Error deleting all data" });
        }
    }
}
