using AAI.Application.Common.Interfaces;
using AAI.Domain.Entities;
using AAI.Domain.Enums;
using AAI.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AAI.Application.Auth.Commands.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, RegisterResponse>
{
    private readonly IUserProfileRepository _userProfileRepository;
    private readonly IPortfolioRepository _portfolioRepository;
    private readonly IPasswordHashingService _passwordHashingService;
    private readonly IJwtTokenService _jwtTokenService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<RegisterCommandHandler> _logger;

    public RegisterCommandHandler(
        IUserProfileRepository userProfileRepository,
        IPortfolioRepository portfolioRepository,
        IPasswordHashingService passwordHashingService,
        IJwtTokenService jwtTokenService,
        IUnitOfWork unitOfWork,
        ILogger<RegisterCommandHandler> logger)
    {
        _userProfileRepository = userProfileRepository;
        _portfolioRepository = portfolioRepository;
        _passwordHashingService = passwordHashingService;
        _jwtTokenService = jwtTokenService;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<RegisterResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        try
        {
            // Check if a user already exists (single-user application)
            var existingUser = await _userProfileRepository.AnyUserExistsAsync(cancellationToken);
            if (existingUser)
            {
                _logger.LogWarning("Attempt to register when user already exists");
                return new RegisterResponse
                {
                    Success = false,
                    Message = "Usuário já cadastrado. Esta é uma aplicação single-user."
                };
            }

            // Hash the PIN
            var passwordHash = _passwordHashingService.HashPassword(request.Pin, out var salt);

            // Parse risk profile
            if (!Enum.TryParse<RiskProfile>(request.RiskProfile, true, out var riskProfile))
            {
                riskProfile = RiskProfile.Moderado;
            }

            // Create UserProfile
            var userProfile = new Domain.Entities.UserProfile
            {
                Id = Guid.NewGuid(),
                RiskProfile = riskProfile,
                InvestmentGoal = request.InvestmentGoal,
                VolatilityTolerance = request.VolatilityTolerance,
                TimeHorizonMonths = request.TimeHorizonMonths,
                RebalanceThresholdPercent = request.RebalanceThresholdPercent,
                TargetAllocationJson = request.TargetAllocationJson,
                PasswordHash = passwordHash,
                PasswordSalt = salt,
                CreatedAt = DateTime.UtcNow
            };

            await _userProfileRepository.AddAsync(userProfile, cancellationToken);

            // Create default Portfolio
            var portfolio = new Portfolio
            {
                Id = Guid.NewGuid(),
                UserId = userProfile.Id,
                Name = "Meu Portfólio",
                Description = "Portfólio principal",
                Currency = "BRL",
                CreatedAt = DateTime.UtcNow
            };

            await _portfolioRepository.AddAsync(portfolio, cancellationToken);

            // Save changes
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            // Generate JWT token
            var token = _jwtTokenService.GenerateToken(userProfile.Id);

            _logger.LogInformation("User registered successfully with ID: {UserId}", userProfile.Id);

            return new RegisterResponse
            {
                UserId = userProfile.Id,
                Token = token,
                Success = true,
                Message = "Usuário cadastrado com sucesso"
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error registering user");
            return new RegisterResponse
            {
                Success = false,
                Message = "Erro ao cadastrar usuário"
            };
        }
    }
}
