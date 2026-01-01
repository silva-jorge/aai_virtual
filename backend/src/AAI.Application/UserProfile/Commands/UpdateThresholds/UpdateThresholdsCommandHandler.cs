using AAI.Application.UserProfile.DTOs;
using AAI.Domain.Interfaces;
using AutoMapper;
using MediatR;
using System.Text.Json;

namespace AAI.Application.UserProfile.Commands.UpdateThresholds;

public class UpdateThresholdsCommandHandler : IRequestHandler<UpdateThresholdsCommand, UserProfileDto>
{
    private readonly IUserProfileRepository _userProfileRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateThresholdsCommandHandler(
        IUserProfileRepository userProfileRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _userProfileRepository = userProfileRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
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
        
        var changesSaved = await _unitOfWork.SaveChangesAsync(cancellationToken);
        Console.WriteLine($"[UpdateThresholds] Changes saved: {changesSaved}");

        return _mapper.Map<UserProfileDto>(userProfile);
    }
}
