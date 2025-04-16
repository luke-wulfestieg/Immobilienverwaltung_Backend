using AutoMapper;
using BE.Application.Restaurants.Commands.CreateRestaurant;
using BE.Application.Restaurants.DTOs;
using BE.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BE.Application.Restaurants.Queries.GetAllRestaurants
{
    public class GetAllRestaurantQueryHandler
         (ILogger<GetAllRestaurantQueryHandler> logger,
        IMapper mapper,
        IRestaurantRepository restaurantRepository) : IRequestHandler<GetAllRestaurantQuery, IEnumerable<RestaurantDto>>
    {
        public async Task<IEnumerable<RestaurantDto>> Handle(GetAllRestaurantQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Getting all Restaurants - {Restaurant}", request);
            var restaurants = await restaurantRepository.GetAllAsync();

            var restaurantDtos = mapper.Map<IEnumerable<RestaurantDto>>(restaurants);

            return restaurantDtos!;
        }
    }
}