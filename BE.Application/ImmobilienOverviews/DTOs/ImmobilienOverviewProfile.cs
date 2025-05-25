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

            CreateMap<CreateImmobilienOverviewCommand, ImmobilienOverview>()
                .ForMember(dest => dest.ImmobilienType, opt => opt.Ignore())
                .ForMember(dest => dest.ImmobilienHausgeld, opt => opt.Ignore())
                .ForMember(dest => dest.ImmobilienHypothek, opt => opt.Ignore())
                .ForMember(dest => dest.Bruttomietrendite, opt => opt.Ignore());


            CreateMap<UpdateImmobilienOverviewCommand, ImmobilienOverview>()
                .ForMember(dest => dest.ImmobilienType, opt => opt.Ignore());

            CreateMap<ImmobilienOverview, ImmobilienOverviewDto>();
        }
    }
}
