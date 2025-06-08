using AutoMapper;
using BE.Application.Gesamtbelastungen.Commands.CreateGesamtbelastung;
using BE.Application.Gesamtbelastungen.Commands.UpdateGesamtbelastung;
using BE.Domain.Entities;

namespace BE.Application.Gesamtbelastungen.DTOs
{
    public class GesamtbelastungProfile : Profile
    {
        public GesamtbelastungProfile()
        {
            CreateMap<CreateGesamtbelastungCommand, Gesamtbelastung>();
            CreateMap<UpdateGesamtbelastungCommand, Ruecklage>();
            CreateMap<Gesamtbelastung, GesamtbelastungDto>();
            CreateMap<GesamtbelastungDto, Gesamtbelastung>();
        }
    }
}
