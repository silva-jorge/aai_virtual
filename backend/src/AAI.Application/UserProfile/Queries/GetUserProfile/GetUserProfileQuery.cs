using AAI.Application.UserProfile.DTOs;
using MediatR;

namespace AAI.Application.UserProfile.Queries.GetUserProfile;

public record GetUserProfileQuery(string UserId) : IRequest<UserProfileDto>;
