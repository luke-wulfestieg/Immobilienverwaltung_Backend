using AutoMapper;
using BE.Application.ImmobilienTypes.Commands.DeleteTypes;
using BE.Domain.Entities;
using BE.Domain.Exceptions;
using BE.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BE.Application.ImmobilienHausgelder.Commands.DeleteHausgeld
{
    public class DeleteImmobilienHausgeldCommandHandler
        (ILogger<DeleteImmobilienHausgeldCommandHandler> logger,
        IMapper mapper,
        IImmobilienHausgeldRepository hausgeldRepository) : IRequestHandler<DeleteImmobilienHausgeldCommand>
    {
        public async Task Handle(DeleteImmobilienHausgeldCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Deleting Hausgeld with {Id} {Type}", request.Id, request);


            var hausgeld = await hausgeldRepository.GetByIdAsync(request.Id);

            if (hausgeld is null)
            {
                throw new NotFoundException(nameof(ImmobilienType), request.Id.ToString());
            }

            await hausgeldRepository.Delete(hausgeld);
        }
    }
}
