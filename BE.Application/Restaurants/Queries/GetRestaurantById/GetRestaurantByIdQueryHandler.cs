using AutoMapper;
using BE.Application.Restaurants.DTOs;
using BE.Domain.Entities;
using BE.Domain.Exceptions;
using BE.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BE.Application.Restaurants.Queries.GetRestaurantById
{
    public class GetRestaurantByIdQueryHandler
        (ILogger<GetRestaurantByIdQuery> logger,
        IMapper mapper,
        IRestaurantRepository restaurantRepository) : IRequestHandler<GetRestaurantByIdQuery, RestaurantDto>
    {
        public async Task<RestaurantDto> Handle(GetRestaurantByIdQuery request, CancellationToken cancellationToken)
        {

            var restaurant = 
                await restaurantRepository.GetByIdAsync(request.Id) ??
                throw new NotFoundException(nameof(Restaurant), request.Id.ToString());
            var restaurantDto = mapper.Map<RestaurantDto>(restaurant);

            return restaurantDto;
        }
    }
}