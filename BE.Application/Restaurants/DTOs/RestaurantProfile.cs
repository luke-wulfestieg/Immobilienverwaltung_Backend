

using AutoMapper;
using BE.Application.Restaurants.Commands.CreateRestaurant;
using BE.Application.Restaurants.Commands.UpdateRestaurant;
using BE.Domain.Entities;

namespace BE.Application.Restaurants.DTOs
{
    public class RestaurantProfile : Profile
    {
        public RestaurantProfile() 
        {
            CreateMap<UpdateRestaurantByIdCommand, Restaurant>();

            CreateMap<CreateRestaurantCommand, Restaurant>()
                .ForMember(d => d.Address, opt => opt.MapFrom(src => new Address
                {
                    City = src.City,
                    PostalCode = src.PostalCode,
                    Street = src.Street
                }));

            CreateMap<Restaurant, RestaurantDto>()
                .ForMember(d => d.City, opt => opt.MapFrom(src => src.Address == null ? null : src.Address.City))
                .ForMember(d => d.PostalCode, opt => opt.MapFrom(src => src.Address == null ? null : src.Address.PostalCode))
                .ForMember(d => d.Street, opt => opt.MapFrom(src => src.Address == null ? null : src.Address.Street))
                .ForMember(d => d.Dishes, opt => opt.MapFrom(src => src.Dishes));

        }

    }
}
