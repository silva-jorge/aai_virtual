using AutoMapper;
using AAI.Application.Auth.DTOs;
using AAI.Application.Portfolio.DTOs;
using AAI.Application.UserProfile.DTOs;
using AAI.Application.Rebalancing.DTOs;
using AAI.Domain.Entities;
using PortfolioEntity = AAI.Domain.Entities.Portfolio;
using PositionEntity = AAI.Domain.Entities.Position;
using UserProfileEntity = AAI.Domain.Entities.UserProfile;
using RecommendationEntity = AAI.Domain.Entities.Recommendation;

namespace AAI.Application.Common.Mappings;

/// <summary>
/// AutoMapper profile for mapping between domain entities and DTOs.
/// Centralizes all entity-to-DTO and DTO-to-entity mappings.
/// </summary>
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Portfolio Mappings
        CreateMap<PortfolioEntity, PortfolioSummaryDto>()
            .ReverseMap();

        CreateMap<PositionEntity, PositionDto>()
            .ReverseMap();

        // User Profile Mappings
        CreateMap<UserProfileEntity, UserProfileDto>()
            .ReverseMap();

        // Recommendation Mappings
        CreateMap<RecommendationEntity, RecommendationDto>()
            .ReverseMap();

        // Auth Mappings
        CreateMap<UserProfileEntity, AuthUserDTO>()
            .ReverseMap();
    }
}
