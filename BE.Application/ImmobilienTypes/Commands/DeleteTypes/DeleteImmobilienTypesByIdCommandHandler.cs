using AutoMapper;
using BE.Domain.Entities;
using BE.Domain.Exceptions;
using BE.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BE.Application.ImmobilienTypes.Commands.DeleteTypes
{
    public class DeleteImmobilienTypesByIdCommandHandler
        (ILogger<DeleteImmobilienTypesByIdCommandHandler> logger,
        IMapper mapper,
        IImmobilienTypeRepository typeRepository) : IRequestHandler<DeleteImmobilienTypesByIdCommand>
    {
        public async Task Handle(DeleteImmobilienTypesByIdCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Deleting ImmobilienType with {Id} {Type}", request.Id, request);


            var type = await typeRepository.GetByIdAsync(request.Id);

            if (type is null)
            {
                throw new NotFoundException(nameof(ImmobilienType), request.Id.ToString());
            }

            await typeRepository.Delete(type);
        }
    }
}
