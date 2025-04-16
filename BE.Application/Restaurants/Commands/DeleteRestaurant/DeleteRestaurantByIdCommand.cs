using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace BE.Application.Restaurants.Commands.DeleteRestaurant
{
    public class DeleteRestaurantByIdCommand(int id) : IRequest
    {
        public int Id { get; } = id;
    }
}
