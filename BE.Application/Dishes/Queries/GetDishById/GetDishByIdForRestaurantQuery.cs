using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE.Application.Dishes.DTOs;
using MediatR;

namespace BE.Application.Dishes.Queries.GetDishById
{
    public class GetDishByIdForRestaurantQuery(int restaurantId, int dishId) : IRequest<DishDto>
    {
        public int RestaurantId { get; } = restaurantId;
        public int DishId { get; } = dishId;

    }
}
