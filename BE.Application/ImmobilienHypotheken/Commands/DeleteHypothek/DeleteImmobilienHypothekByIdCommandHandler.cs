using BE.Domain.Entities;
using BE.Domain.Entities.Hypothek;
using BE.Domain.Exceptions;
using BE.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BE.Application.ImmobilienHypotheken.Commands.DeleteHypothek
{
    public class DeleteImmobilienHypothekByIdCommandHandler
        (ILogger<DeleteImmobilienHypothekByIdCommandHandler> logger,
        IImmobilienHypothekRepository hypothekRepository)
    : IRequestHandler<DeleteImmobilienHypothekByIdCommand>
    {
        public async Task Handle(DeleteImmobilienHypothekByIdCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Deleting Hypothek with {Id}", request.Id);


            var hypothek = await hypothekRepository.GetByIdAsync(request.Id);

            if (hypothek is null)
            {
                throw new NotFoundException(nameof(ImmobilienHypothek), request.Id.ToString());
            }

            await hypothekRepository.Delete(hypothek);
        }
    }
}
