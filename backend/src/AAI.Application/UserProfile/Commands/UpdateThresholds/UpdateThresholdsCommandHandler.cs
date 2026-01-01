using AAI.Application.UserProfile.DTOs;
using AAI.Domain.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;

namespace AAI.Application.UserProfile.Commands.UpdateThresholds;

public class UpdateThresholdsCommandHandler : IRequestHandler<UpdateThresholdsCommand, UserProfileDto>
{
    private readonly IUserProfileRepository _userProfileRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IMemoryCache _cache;

    public UpdateThresholdsCommandHandler(
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

    public async Task<UserProfileDto> Handle(UpdateThresholdsCommand request, CancellationToken cancellationToken)
    {
        var userId = Guid.Parse(request.UserId);
        var userProfile = await _userProfileRepository.GetByIdAsync(userId, cancellationToken);
        
        if (userProfile == null)
        {
            throw new KeyNotFoundException($"User profile with ID {request.UserId} not found");
        }

        // Validate JSON format
        try
        {
            JsonDocument.Parse(request.TargetAllocationJson);
        }
        catch (JsonException)
        {
            throw new ArgumentException("TargetAllocationJson is not valid JSON");
        }

        // Update thresholds
        userProfile.RebalanceThresholdPercent = request.RebalanceThresholdPercent;
        userProfile.TargetAllocationJson = request.TargetAllocationJson;
        userProfile.UpdatedAt = DateTime.UtcNow;

        await _userProfileRepository.UpdateAsync(userProfile, cancellationToken);
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);

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
