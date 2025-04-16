using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE.Application.Restaurants.DTOs;
using MediatR;

namespace BE.Application.Restaurants.Queries.GetRestaurantById
{
    public class GetRestaurantByIdQuery : IRequest<RestaurantDto>
    {
        public GetRestaurantByIdQuery(int id)
        {   
            Id = id;
        }

        public int Id { get; }
    }
}
