using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace BE.Application.Dishes.Commands.DeleteAllDishes
{
    public class DeleteAllDishesFromRestaurantCommand(int restaurantId) : IRequest
    {
        public int RestaurantId { get; } = restaurantId;
    }
}
