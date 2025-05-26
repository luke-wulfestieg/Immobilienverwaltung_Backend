using AutoMapper;
using BE.Domain.Entities;
using BE.Domain.Exceptions;
using BE.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BE.Application.ImmobilienHausgelder.Commands.UpdateHausgeld
{
    public class UpdateImmobilienHausgeldByIdCommandHandler
        (ILogger<UpdateImmobilienHausgeldByIdCommandHandler> logger,
        IMapper mapper,
        IBruttomietrenditeRepository bruttomietrenditeRepository,
        IImmobilienOverviewRepository overviewRepository,
        IImmobilienHausgeldRepository hausgeldRepository) : IRequestHandler<UpdateImmobilienHausgeldByIdCommand>
    {
        public async Task Handle(UpdateImmobilienHausgeldByIdCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Updating Hausgeld with ID: {Id}. New Values: HausgeldProMonat: {HausgeldProMonat}, HausgeldProJahr: {HausgeldProJahr} ...", request.Id, request.Hausgeld.ProMonat, request.Hausgeld.ProJahr);

            var hausgeld = await hausgeldRepository.GetByIdAsync(request.Id);

            if (hausgeld == null)
            {
                throw new NotFoundException(nameof(ImmobilienHausgeld), request.Id.ToString());
            }

            mapper.Map(request, hausgeld);

            var bruttomietrendite = await bruttomietrenditeRepository.GetByIdAsync(request.Id);
            bruttomietrendite.UmlagefaehigesHausgeld.InProzent = hausgeld.UmlagefaehigesHausgeld.InProzent;
            bruttomietrendite.UmlagefaehigesHausgeld.ProMonat = hausgeld.UmlagefaehigesHausgeld.ProMonat;
            bruttomietrendite.UmlagefaehigesHausgeld.ProJahr = hausgeld.UmlagefaehigesHausgeld.ProJahr;

            await bruttomietrenditeRepository.SaveChanges();
            await hausgeldRepository.SaveChanges();
        }
    }
}
