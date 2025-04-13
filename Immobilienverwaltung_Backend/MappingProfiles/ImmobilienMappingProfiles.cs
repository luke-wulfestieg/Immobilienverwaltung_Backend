using AutoMapper;
using Immobilienverwaltung_Backend.Features.Immobilien_Overview.Models;
using Immobilienverwaltung_Backend.Features.Immobilien_Overview.DTOs;

namespace Immobilienverwaltung_Backend.MappingProfiles
{
    public class ImmobilienMappingProfile : Profile
    {
        public ImmobilienMappingProfile()
        {
            // Mapping for ImmobilienOverview -> Immobilien_Overview_DTO
            CreateMap<ImmobilienOverview, Immobilien_Overview_DTO>()
                .ForMember(dest => dest.ImmobilienType,
                           opt => opt.MapFrom(src => new Immobilien_Type_DTO
                           {
                               Id = src.ImmobilienType.Id,
                               ImmobilienType = src.ImmobilienType.ImmobilienType
                           }))
                .ForMember(dest => dest.ImmobilienHausgeld,
                            opt => opt.MapFrom(src => new Immobilien_Hausgeld_DTO
                            {
                                Id = src.ImmobilienHausgeld.Id,
                                Hausgeld = src.ImmobilienHausgeld.Hausgeld,
                                Nicht_Umlagefaehiges_Hausgeld = src.ImmobilienHausgeld.Nicht_Umlagefaehiges_Hausgeld,
                                Umlagefaehiges_Hausgeld = src.ImmobilienHausgeld.Umlagefaehiges_Hausgeld,
                                ImmobilienOverviewId = src.ImmobilienHausgeld.ImmobilienOverviewId
                            }));

            // Immobilien_Overview_DTO -> ImmobilienOverview
            CreateMap<Immobilien_Overview_DTO, ImmobilienOverview>()
    .ForMember(dest => dest.ImmobilienType,
        opt => opt.MapFrom(src => new Immobilien_Type { Id = src.ImmobilienType.Id })) // Only map the ID
    .ForMember(dest => dest.ImmobilienHausgeld,
        opt => opt.MapFrom(src => new Immobilien_Hausgeld
        {
            Id = src.ImmobilienHausgeld.Id,
            Hausgeld = src.ImmobilienHausgeld.Hausgeld,
            Nicht_Umlagefaehiges_Hausgeld = src.ImmobilienHausgeld.Nicht_Umlagefaehiges_Hausgeld,
            Umlagefaehiges_Hausgeld = src.ImmobilienHausgeld.Umlagefaehiges_Hausgeld,
            ImmobilienOverviewId = src.ImmobilienHausgeld.ImmobilienOverviewId
        }));


            // Mapping for Immobilien_Type_DTO -> Immobilien_Type
            CreateMap<Immobilien_Type_DTO, Immobilien_Type>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.ImmobilienType, opt => opt.MapFrom(src => src.ImmobilienType));

            // Mapping for Immobilien_Type -> Immobilien_Type_DTO
            CreateMap<Immobilien_Type, Immobilien_Type_DTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.ImmobilienType, opt => opt.MapFrom(src => src.ImmobilienType));


            // Mapping for Immobilien_Hausgeld -> Immobilien_Hausgeld_DTO
            CreateMap<Immobilien_Hausgeld, Immobilien_Hausgeld_DTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Hausgeld, opt => opt.MapFrom(src => src.Hausgeld))
                .ForMember(dest => dest.Nicht_Umlagefaehiges_Hausgeld, opt => opt.MapFrom(src => src.Nicht_Umlagefaehiges_Hausgeld))
                .ForMember(dest => dest.Umlagefaehiges_Hausgeld, opt => opt.MapFrom(src => src.Umlagefaehiges_Hausgeld))
                .ForMember(dest => dest.ImmobilienOverviewId, opt => opt.MapFrom(src => src.ImmobilienOverviewId));

            // Mapping for Immobilien_Hausgeld_DTO -> Immobilien_Hausgeld
            CreateMap<Immobilien_Hausgeld_DTO, Immobilien_Hausgeld>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Hausgeld, opt => opt.MapFrom(src => src.Hausgeld))
                .ForMember(dest => dest.Nicht_Umlagefaehiges_Hausgeld, opt => opt.MapFrom(src => src.Nicht_Umlagefaehiges_Hausgeld))
                .ForMember(dest => dest.Umlagefaehiges_Hausgeld, opt => opt.MapFrom(src => src.Umlagefaehiges_Hausgeld))
                .ForMember(dest => dest.ImmobilienOverviewId, opt => opt.MapFrom(src => src.ImmobilienOverviewId));
        }
    }
}
