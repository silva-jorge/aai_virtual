using AAI.Application.UserProfile.DTOs;
using AutoMapper;

namespace AAI.Application.UserProfile.Mappings;

public class UserProfileMappingProfile : Profile
{
    public UserProfileMappingProfile()
    {
        CreateMap<Domain.Entities.UserProfile, UserProfileDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()))
            .ForMember(dest => dest.RiskProfile, opt => opt.MapFrom(src => src.RiskProfile.ToString()))
            .ForMember(dest => dest.InvestmentGoal, opt => opt.MapFrom(src => src.InvestmentGoal))
            .ForMember(dest => dest.VolatilityTolerance, opt => opt.MapFrom(src => src.VolatilityTolerance))
            .ForMember(dest => dest.TimeHorizonMonths, opt => opt.MapFrom(src => src.TimeHorizonMonths))
            .ForMember(dest => dest.RebalanceThresholdPercent, opt => opt.MapFrom(src => src.RebalanceThresholdPercent))
            .ForMember(dest => dest.TargetAllocationJson, opt => opt.MapFrom(src => src.TargetAllocationJson))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt.HasValue ? src.UpdatedAt.Value : src.CreatedAt));
    }
}
