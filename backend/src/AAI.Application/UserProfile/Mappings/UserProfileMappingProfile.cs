using AAI.Application.UserProfile.DTOs;
using AutoMapper;

namespace AAI.Application.UserProfile.Mappings;

public class UserProfileMappingProfile : Profile
{
    public UserProfileMappingProfile()
    {
        CreateMap<Domain.Entities.UserProfile, UserProfileDto>()
            .ForMember(dest => dest.RiskProfile, opt => opt.MapFrom(src => src.RiskProfile.ToString()));
    }
}
