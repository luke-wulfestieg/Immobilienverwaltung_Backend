using AutoMapper;
using BE.Application.ImmobilienHypotheken.Commands.CreateHypothek;
using BE.Application.ImmobilienHypotheken.Commands.UpdateHypothek;
using BE.Application.Ruecklagen.Commands.CreateRuecklagen;
using BE.Application.Ruecklagen.Commands.UpdateRuecklagen;
using BE.Domain.Entities;
using BE.Domain.Entities.Hypothek;

namespace BE.Application.Ruecklagen.DTOs
{
    public class RuecklagenProfile : Profile
    {
        public RuecklagenProfile() {
            CreateMap<CreateRuecklagenCommand, Ruecklage>();
            CreateMap<UpdateRuecklagenCommand, Ruecklage>();

            CreateMap<Ruecklage, RuecklagenDto>();
            CreateMap<RuecklagenDto, Ruecklage>();
        }
    }
}
