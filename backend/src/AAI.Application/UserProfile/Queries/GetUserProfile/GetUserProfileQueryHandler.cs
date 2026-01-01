using AAI.Application.UserProfile.DTOs;
using AAI.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AAI.Application.UserProfile.Queries.GetUserProfile;

public class GetUserProfileQueryHandler : IRequestHandler<GetUserProfileQuery, UserProfileDto>
{
    private readonly IUserProfileRepository _userProfileRepository;
    private readonly IMapper _mapper;

    public GetUserProfileQueryHandler(
        IUserProfileRepository userProfileRepository,
        IMapper mapper)
    {
        _userProfileRepository = userProfileRepository;
        _mapper = mapper;
    }

    public async Task<UserProfileDto> Handle(GetUserProfileQuery request, CancellationToken cancellationToken)
    {
        var userId = Guid.Parse(request.UserId);
        var userProfile = await _userProfileRepository.GetByIdAsync(userId, cancellationToken);
        
        if (userProfile == null)
        {
            throw new KeyNotFoundException($"User profile with ID {request.UserId} not found");
        }

        return _mapper.Map<UserProfileDto>(userProfile);
    }
}
