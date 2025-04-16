using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE.Application.Restaurants.DTOs;
using MediatR;

namespace BE.Application.Restaurants.Queries.GetAllRestaurants
{
    public class GetAllRestaurantQuery : IRequest<IEnumerable<RestaurantDto>>
    {
    }
}
