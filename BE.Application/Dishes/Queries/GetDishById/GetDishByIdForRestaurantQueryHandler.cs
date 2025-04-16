using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BE.Application.Dishes.DTOs;
using BE.Application.Dishes.Queries.GetDishesForRestaurant;
using BE.Domain.Entities;
using BE.Domain.Exceptions;
using BE.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BE.Application.Dishes.Queries.GetDishById
{
    public class GetDishByIdForRestaurantQueryHandler(ILogger<GetDishByIdForRestaurantQueryHandler> logger,
        IMapper mapper,
        IRestaurantRepository restaurantRepository) : IRequestHandler<GetDishByIdForRestaurantQuery, DishDto>
    {
        public async Task<DishDto> Handle(GetDishByIdForRestaurantQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Retrieving Dish by ID: {DishId} for Restaurant by id: {RestaurantId}", request.DishId ,request.RestaurantId);

            var restaurant = await restaurantRepository.GetByIdAsync(request.RestaurantId);

            if (restaurant == null)
            {
                throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());
            }

            var dish = restaurant.Dishes.FirstOrDefault(d => d.Id == request.DishId);
            if (dish == null)
            {
                throw new NotFoundException(nameof(Dish), request.DishId.ToString());
            }

            var result = mapper.Map<DishDto>(dish);

            return result;
        }
    }
}
