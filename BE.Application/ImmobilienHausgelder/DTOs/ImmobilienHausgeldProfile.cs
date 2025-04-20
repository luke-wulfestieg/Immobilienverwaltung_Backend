using AutoMapper;
using BE.Application.ImmobilienHausgelder.DTOs;
using BE.Application.ImmobilienHausgelder.Commands.CreateHausgeld;
using BE.Domain.Entities;
using BE.Application.ImmobilienHausgelder.Commands.UpdateHausgeld;

namespace BE.Application.ImmoilienHausgelder.DTOs
{
    public class ImmobilienHausgeldProfile : Profile
    {
        public ImmobilienHausgeldProfile()
        {
            CreateMap<CreateImmobilienHausgeldCommand, ImmobilienHausgeld>();

            CreateMap<UpdateImmobilienHausgeldByIdCommand, ImmobilienHausgeld>();

            CreateMap<ImmobilienHausgeld, ImmobilienHausgeldDto>();

            CreateMap<ImmobilienHausgeldDto, ImmobilienHausgeld>();
        }
    }
}
