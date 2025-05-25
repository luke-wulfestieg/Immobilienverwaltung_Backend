using AutoMapper;
using BE.Application.Bruttomietrenditen.Commands.CreateBruttomietrendite;
using BE.Application.Bruttomietrenditen.Commands.UpdateBruttomietrendite;
using BE.Domain.Entities;

namespace BE.Application.Bruttomietrenditen.DTOs
{
    public class BruttomietrenditeProfile: Profile
    {
        public BruttomietrenditeProfile() 
        {
            CreateMap<CreateBruttomietrenditeCommand, Bruttomietrendite>();

            CreateMap<UpdateBruttomietrenditeByIdCommand, Bruttomietrendite>();

            CreateMap<Bruttomietrendite, BruttomietrenditeDto>();

            CreateMap<BruttomietrenditeDto, Bruttomietrendite>();
        }
    }
}
