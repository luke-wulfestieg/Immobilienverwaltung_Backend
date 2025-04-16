using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BE.Application.Restaurants.DTOs;
using BE.Domain.Entities;
using BE.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BE.Application.Restaurants.Commands.CreateRestaurant
{
    public class CreateRestaurantCommandHandler
        (ILogger<CreateRestaurantCommandHandler> logger, 
        IMapper mapper, 
        IRestaurantRepository restaurantRepository) 
        : IRequestHandler<CreateRestaurantCommand, int>
    {
        public async Task<int> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Creating Restaurnat {Restaurant}", request);

            var restaurant = mapper.Map<Restaurant>(request);

            int id = await restaurantRepository.Create(restaurant);

            return id;
        }
    }
}
