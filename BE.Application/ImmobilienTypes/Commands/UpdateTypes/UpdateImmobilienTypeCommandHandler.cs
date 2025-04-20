using AutoMapper;
using BE.Domain.Entities;
using BE.Domain.Exceptions;
using BE.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BE.Application.ImmobilienTypes.Commands.UpdateTypes
{
    public class UpdateImmobilienTypeCommandHandler
        (ILogger<UpdateImmobilienTypeCommandHandler> logger,
        IMapper mapper,
        IImmobilienTypeRepository typeRepository) : IRequestHandler<UpdateImmobilienTypeCommand>
    {
        public async Task Handle(UpdateImmobilienTypeCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Updating ImmobilienType with {Id} {Type}", request.Id, request);

            var type = await typeRepository.GetByIdAsync(request.Id);

            if (type == null)
            {
                throw new NotFoundException(nameof(ImmobilienType), request.Id.ToString());
            }

            mapper.Map(request, type);
            //restaurant.Name = request.Name;
            //restaurant.Description = request.Description;
            //restaurant.HasDelivery = request.HasDelivery;

            await typeRepository.SaveChanges();
        }
    }
}
