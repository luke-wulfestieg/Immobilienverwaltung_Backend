using AutoMapper;
using BE.Domain.Entities;
using BE.Domain.Exceptions;
using BE.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BE.Application.Restaurants.Commands.DeleteRestaurant
{
    public class DeleteRestaurantByIdCommandHandler
         (ILogger<DeleteRestaurantByIdCommandHandler> logger,
        IMapper mapper,
        IRestaurantRepository restaurantRepository) : IRequestHandler<DeleteRestaurantByIdCommand>
    {
        public async Task Handle(DeleteRestaurantByIdCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Deleting Restaurant with {Id} {Restaurant}", request.Id,  request);


            var restaurant = await restaurantRepository.GetByIdAsync(request.Id);

            if (restaurant is null) {
                throw new NotFoundException(nameof(Restaurant), request.Id.ToString());
            }

            await restaurantRepository.Delete(restaurant);
        }
    }
}
