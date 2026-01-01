using AAI.Application.UserProfile.DTOs;
using AAI.Domain.Enums;
using AAI.Domain.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace AAI.Application.UserProfile.Commands.UpdateRiskProfile;

public class UpdateRiskProfileCommandHandler : IRequestHandler<UpdateRiskProfileCommand, UserProfileDto>
{
    private readonly IUserProfileRepository _userProfileRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IMemoryCache _cache;

    public UpdateRiskProfileCommandHandler(
        IUserProfileRepository userProfileRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IMemoryCache cache)
    {
        _userProfileRepository = userProfileRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _cache = cache;
    }

    public async Task<UserProfileDto> Handle(UpdateRiskProfileCommand request, CancellationToken cancellationToken)
    {
        var userId = Guid.Parse(request.UserId);
        var userProfile = await _userProfileRepository.GetByIdAsync(userId, cancellationToken);
        
        if (userProfile == null)
        {
            throw new KeyNotFoundException($"User profile with ID {request.UserId} not found");
        }

        // Parse and validate RiskProfile enum
        if (!Enum.TryParse<RiskProfile>(request.RiskProfile, true, out var riskProfile))
        {
            throw new ArgumentException($"Invalid risk profile: {request.RiskProfile}");
        }

        // Update profile
        userProfile.RiskProfile = riskProfile;
        userProfile.InvestmentGoal = request.InvestmentGoal;
        userProfile.VolatilityTolerance = request.VolatilityTolerance;
        userProfile.TimeHorizonMonths = request.TimeHorizonMonths;
        userProfile.UpdatedAt = DateTime.UtcNow;

        await _userProfileRepository.UpdateAsync(userProfile, cancellationToken);
        
        var changesSaved = await _unitOfWork.SaveChangesAsync(cancellationToken);
        Console.WriteLine($"[UpdateRiskProfile] Changes saved: {changesSaved}");

        // Invalidate cache for GetUserProfileQuery
        InvalidateProfileCache(request.UserId);

        return _mapper.Map<UserProfileDto>(userProfile);
    }

    private void InvalidateProfileCache(string userId)
    {
        // Clear all cache - simple but effective solution
        // In production, consider using cache tags or more sophisticated invalidation
        if (_cache is MemoryCache memCache)
        {
            memCache.Compact(1.0); // Remove 100% of cache entries
        }
    }
}
