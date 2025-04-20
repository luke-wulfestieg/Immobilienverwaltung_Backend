using System;using AutoMapper;
using BE.Application.ImmobilienTypes.Commands.CreateTypes;
using BE.Application.ImmobilienTypes.Commands.UpdateTypes;
using BE.Domain.Entities;

namespace BE.Application.ImmobilienTypes.DTOs
{
    public class ImmobilienTypeProfile : Profile
    {
        public ImmobilienTypeProfile()
        {
            CreateMap<CreateImmobilienTypeCommand, ImmobilienType>();

            CreateMap<UpdateImmobilienTypeCommand, ImmobilienType>();

            CreateMap<ImmobilienType, ImmobilienTypeDto>();

        }
    }
}
