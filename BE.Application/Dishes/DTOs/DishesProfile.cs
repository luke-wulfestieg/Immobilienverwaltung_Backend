using AutoMapper;
using BE.Application.Dishes.Commands.CreateDish;
using BE.Domain.Entities;

namespace BE.Application.Dishes.DTOs
{
    public class DishesProfile : Profile
    {
        public DishesProfile() 
        {
            CreateMap<CreateDishCommand, Dish>();

            CreateMap<Dish, DishDto>();
        }
    }
}
