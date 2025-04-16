using AutoMapper;
using BE.Application.Dishes.DTOs;
using BE.Domain.Entities;
using BE.Domain.Exceptions;
using BE.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BE.Application.Dishes.Queries.GetDishesForRestaurant
{
    public class GetDishesForRestaurantQueryHandler(ILogger<GetDishesForRestaurantQueryHandler> logger,
        IMapper mapper,
        IRestaurantRepository restaurantRepository) : IRequestHandler<GetDishesForRestaurantQuery, IEnumerable<DishDto>>
    {
        public async Task<IEnumerable<DishDto>> Handle(GetDishesForRestaurantQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Retrieving Dishes for Restaurant by id: {RestaurantId}", request.RestaurantId);

            var restaurant = await restaurantRepository.GetByIdAsync(request.RestaurantId);

            if (restaurant == null) {
                throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());
            }

            var results = mapper.Map<IEnumerable<DishDto>>(restaurant.Dishes);

            return results;
        }
    }
}
