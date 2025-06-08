using AutoMapper;
using BE.Application.Ruecklagen.Commands.CreateRuecklagen;
using BE.Application.Ruecklagen.Commands.UpdateRuecklagen;
using BE.Domain.Entities;

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
