using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BE.Domain.Entities;
using BE.Domain.Exceptions;
using BE.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BE.Application.Dishes.Commands.DeleteAllDishes
{
    internal class DeleteAllDishesFromRestaurantCommandHandler(ILogger<DeleteAllDishesFromRestaurantCommandHandler> logger,
        IMapper mapper,
        IRestaurantRepository restaurantRepository,
        IDishesRepository dishesRepository
        ) : IRequestHandler<DeleteAllDishesFromRestaurantCommand>
    {
        public async Task Handle(DeleteAllDishesFromRestaurantCommand request, CancellationToken cancellationToken)
        {
            logger.LogWarning("Removing all Dishes from Restaurant with ID:", request.RestaurantId);
            var restaurant = await restaurantRepository.GetByIdAsync(request.RestaurantId);

            if (restaurant == null)
            {
                throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());
            }

            await dishesRepository.DeleteAll(restaurant.Dishes);


        }
    }
}
