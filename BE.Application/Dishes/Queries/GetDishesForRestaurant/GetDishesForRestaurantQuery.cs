using BE.Application.Dishes.DTOs;
using MediatR;

namespace BE.Application.Dishes.Queries.GetDishesForRestaurant
{
    public class GetDishesForRestaurantQuery(int restaurantId) : IRequest<IEnumerable<DishDto>>
    {
        public int RestaurantId { get; } = restaurantId;
    }
}
