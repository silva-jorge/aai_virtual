using AAI.Application.Auth.Commands.Login;
using AAI.Application.Auth.Commands.Register;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AAI.WebAPI.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<AuthController> _logger;

    public AuthController(IMediator mediator, ILogger<AuthController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Registra um novo usuário com PIN (aplicação single-user).
    /// </summary>
    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromBody] RegisterCommand command)
    {
        var response = await _mediator.Send(command);

        if (!response.Success)
        {
            return BadRequest(new { message = response.Message });
        }

        return Ok(response);
    }

    /// <summary>
    /// Realiza login com PIN.
    /// </summary>
    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginCommand command)
    {
        var response = await _mediator.Send(command);

        if (!response.Success)
        {
            return Unauthorized(new { message = response.Message });
        }

        return Ok(response);
    }

    /// <summary>
    /// Verifica se o token JWT é válido (endpoint protegido).
    /// </summary>
    [HttpGet("verify")]
    [Authorize]
    public IActionResult VerifyToken()
    {
        var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        
        return Ok(new
        {
            valid = true,
            userId,
            message = "Token válido"
        });
    }

    /// <summary>
    /// Verifica se já existe um usuário cadastrado.
    /// </summary>
    [HttpGet("check-user")]
    [AllowAnonymous]
    public async Task<IActionResult> CheckUserExists()
    {
        // TODO: Implement this query in Application layer
        // For now, return a placeholder
        return Ok(new { userExists = false });
    }
}
