using BE.Domain.Repositories;
using BE.Domain.Exceptions;

using MediatR;
using Microsoft.Extensions.Logging;
using BE.Domain.Entities;
using AutoMapper;

namespace BE.Application.Dishes.Commands.CreateDish;

public class CreateDishCommandHandler(ILogger<CreateDishCommandHandler> logger, 
    IMapper mapper,
    IRestaurantRepository restaurantRepository,
    IDishesRepository dishesRepository) : IRequestHandler<CreateDishCommand, int>
{
    public async Task<int> Handle(CreateDishCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating a new {@DishRequest}", request);

        var restaurant = await restaurantRepository.GetByIdAsync(request.RestaurantId);

        if (restaurant is null)
        {
            throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());
        }
        var dish = mapper.Map<Dish>(request);

        return await dishesRepository.Create(dish);
    }
}
