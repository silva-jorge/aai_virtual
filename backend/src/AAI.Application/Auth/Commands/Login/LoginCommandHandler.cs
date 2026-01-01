using AAI.Application.Common.Interfaces;
using AAI.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AAI.Application.Auth.Commands.Login;

public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponse>
{
    private readonly IUserProfileRepository _userProfileRepository;
    private readonly IPasswordHashingService _passwordHashingService;
    private readonly IJwtTokenService _jwtTokenService;
    private readonly ILogger<LoginCommandHandler> _logger;

    public LoginCommandHandler(
        IUserProfileRepository userProfileRepository,
        IPasswordHashingService passwordHashingService,
        IJwtTokenService jwtTokenService,
        ILogger<LoginCommandHandler> logger)
    {
        _userProfileRepository = userProfileRepository;
        _passwordHashingService = passwordHashingService;
        _jwtTokenService = jwtTokenService;
        _logger = logger;
    }

    public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        try
        {
            // Get the first (and should be only) user
            var user = await _userProfileRepository.GetFirstUserAsync(cancellationToken);

            if (user == null)
            {
                _logger.LogWarning("Login attempt but no user exists");
                return new LoginResponse
                {
                    Success = false,
                    Message = "Nenhum usuário cadastrado. Por favor, faça o cadastro primeiro."
                };
            }

            // Verify PIN
            var isValidPin = _passwordHashingService.VerifyPassword(
                request.Pin, 
                user.PasswordHash, 
                user.PasswordSalt);

            if (!isValidPin)
            {
                _logger.LogWarning("Failed login attempt for user {UserId}", user.Id);
                return new LoginResponse
                {
                    Success = false,
                    Message = "PIN incorreto"
                };
            }

            // Generate JWT token
            var token = _jwtTokenService.GenerateToken(user.Id);

            _logger.LogInformation("User {UserId} logged in successfully", user.Id);

            return new LoginResponse
            {
                UserId = user.Id,
                Token = token,
                Success = true,
                Message = "Login realizado com sucesso",
                UserProfile = new UserProfileDto
                {
                    Id = user.Id,
                    RiskProfile = user.RiskProfile.ToString(),
                    InvestmentGoal = user.InvestmentGoal,
                    VolatilityTolerance = user.VolatilityTolerance,
                    TimeHorizonMonths = user.TimeHorizonMonths,
                    RebalanceThresholdPercent = user.RebalanceThresholdPercent,
                    TargetAllocationJson = user.TargetAllocationJson
                }
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during login");
            return new LoginResponse
            {
                Success = false,
                Message = "Erro ao realizar login"
            };
        }
    }
}
