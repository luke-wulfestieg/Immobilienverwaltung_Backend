using AutoMapper;
using BE.Domain.Entities.Hypothek;
using BE.Domain.Exceptions;
using BE.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BE.Application.ImmobilienHypotheken.Commands.UpdateHypothek
{
    public class UpdateImmobilienHypothekByIdCommandHandler
        (ILogger<UpdateImmobilienHypothekByIdCommandHandler> logger,
        IMapper mapper,
        IImmobilienHypothekRepository hypothekRepository) : IRequestHandler<UpdateImmobilienHypothekByIdCommand>
    {
        public async Task Handle(UpdateImmobilienHypothekByIdCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Updating Hypothek with ID: {Id}.", request.Id);

            var hypothek = await hypothekRepository.GetByIdAsync(request.Id);

            if (hypothek == null)
            {
                throw new NotFoundException(nameof(ImmobilienHypothek), request.Id.ToString());
            }

            mapper.Map(request, hypothek);

            await hypothekRepository.SaveChanges();
        }
    }
}
