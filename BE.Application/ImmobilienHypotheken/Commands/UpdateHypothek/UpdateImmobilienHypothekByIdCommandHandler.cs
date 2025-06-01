using AutoMapper;
using BE.Domain.Entities;
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
        IImmobilienOverviewRepository overviewRepository,
        IBruttomietrenditeRepository bruttomietrenditeRepository,
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

            //TODO: Better update approach for updating kaufpreis
            //if Hypothek kaufpreis has changed
            var overview = await overviewRepository.GetByIdAsync(request.Id);
            overview.Kaufpreis = hypothek.Kaufpreis;

            var bruttomietrendite = await bruttomietrenditeRepository.GetByIdAsync(request.Id);
            bruttomietrendite.Kaufpreis = hypothek.Kaufpreis;

            await bruttomietrenditeRepository.SaveChanges();
            await hypothekRepository.SaveChanges();
            await overviewRepository.SaveChanges();
        }
    }
}
