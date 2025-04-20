using AutoMapper;
using BE.Application.ImmobilienOverviews.Commands.CreateOverview;
using BE.Application.ImmobilienOverviews.Commands.UpdateOverviewById;
using BE.Domain.Entities;

namespace BE.Application.ImmobilienOverviews.DTOs
{
    public class ImmobilienOverviewProfile : Profile
    {
        public ImmobilienOverviewProfile()
        {
    
            CreateMap<UpdateImmobilienOverviewCommand, ImmobilienOverview>();

            CreateMap<CreateImmobilienOverviewCommand, ImmobilienOverview>();

            CreateMap<ImmobilienOverview, ImmobilienOverviewDto>()
                .ForMember(dest => dest.ImmobilienType, opt => opt.MapFrom(src => src.ImmobilienType))
                .ForMember(dest => dest.ImmobilienHausgeld, opt => opt.MapFrom(src => src.ImmobilienHausgeld));

        }
    }
}
