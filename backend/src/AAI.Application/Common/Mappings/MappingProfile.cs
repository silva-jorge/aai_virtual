using AutoMapper;
using AAI.Application.Auth.DTOs;
using AAI.Application.Portfolio.DTOs;
using AAI.Application.UserProfile.DTOs;
using AAI.Application.Rebalancing.DTOs;
using AAI.Domain.Entities;

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
        CreateMap<Portfolio, PortfolioSummaryDTO>()
            .ForMember(d => d.TotalValue, o => o.MapFrom(s => s.Positions.Sum(p => p.CurrentValue)))
            .ForMember(d => d.TotalCost, o => o.MapFrom(s => s.Positions.Sum(p => p.AverageCost * p.Quantity)))
            .ReverseMap();

        CreateMap<Portfolio, PortfolioDetailDTO>()
            .ReverseMap();

        CreateMap<Position, PositionDTO>()
            .ReverseMap();

        CreateMap<Asset, AssetDTO>()
            .ReverseMap();

        CreateMap<Transaction, TransactionDTO>()
            .ReverseMap();

        // User Profile Mappings
        CreateMap<UserProfile, UserProfileDTO>()
            .ReverseMap();

        CreateMap<UserProfile, UserProfileDetailDTO>()
            .ReverseMap();

        // Recommendation Mappings
        CreateMap<Recommendation, RecommendationDTO>()
            .ReverseMap();

        CreateMap<Recommendation, RecommendationDetailDTO>()
            .ReverseMap();

        // Auth Mappings
        CreateMap<UserProfile, AuthUserDTO>()
            .ReverseMap();
    }
}
