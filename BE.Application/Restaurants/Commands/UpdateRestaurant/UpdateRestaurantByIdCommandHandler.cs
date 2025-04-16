using AutoMapper;
using BE.Application.Restaurants.Commands.DeleteRestaurant;
using BE.Domain.Entities;
using BE.Domain.Exceptions;
using BE.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BE.Application.Restaurants.Commands.UpdateRestaurant
{
    public class UpdateRestaurantByIdCommandHandler
        (ILogger<UpdateRestaurantByIdCommandHandler> logger,
        IMapper mapper,
        IRestaurantRepository restaurantRepository) : IRequestHandler<UpdateRestaurantByIdCommand>
    {
        public async Task Handle(UpdateRestaurantByIdCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Updating Restaurant with {Id} {Restaurant}", request.Id,  request);

            var restaurant = await restaurantRepository.GetByIdAsync(request.Id);

            if (restaurant == null)
            {
                throw new NotFoundException(nameof(Restaurant), request.Id.ToString());
            }

            mapper.Map(request, restaurant);
            //restaurant.Name = request.Name;
            //restaurant.Description = request.Description;
            //restaurant.HasDelivery = request.HasDelivery;

            await restaurantRepository.SaveChanges();
        }
    }
}