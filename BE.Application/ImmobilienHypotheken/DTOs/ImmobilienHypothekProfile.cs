using AutoMapper;
using BE.Application.ImmobilienHypotheken.Commands.CreateHypothek;
using BE.Application.ImmobilienHypotheken.Commands.UpdateHypothek;
using BE.Domain.Entities.Hypothek;

namespace BE.Application.ImmobilienHypotheken.DTOs
{
    public class ImmobilienHypothekProfile : Profile
    {
        public ImmobilienHypothekProfile()
        {
            CreateMap<CreateImmobilienHypothekCommand, ImmobilienHypothek>();

            CreateMap<UpdateImmobilienHypothekByIdCommand, ImmobilienHypothek>();

            CreateMap<ImmobilienHypothek, ImmobilienHypothekDto>();

            CreateMap<ImmobilienHypothekDto, ImmobilienHypothek>();
        }
    }
}
