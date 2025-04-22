using AutoMapper;
using BE.Domain.Entities;
using BE.Domain.Exceptions;
using BE.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BE.Application.ImmobilienOverviews.Commands.UpdateOverviewById
{
    public class UpdateImmobilienOverviewCommandHandler(
            ILogger<UpdateImmobilienOverviewCommandHandler> logger,
            IMapper mapper,
            IImmobilienOverviewRepository overviewRepository,
            IImmobilienTypeRepository typeRepository,
            IImmobilienHausgeldRepository hausgeldRepository)
        : IRequestHandler<UpdateImmobilienOverviewCommand>
    {
        public async Task Handle(UpdateImmobilienOverviewCommand request, CancellationToken cancellationToken)
        {
            var overview = await overviewRepository.GetByIdAsync(request.Id)
                ?? throw new NotFoundException(nameof(ImmobilienOverview), request.Id.ToString());

            mapper.Map(request, overview);

            var type = await typeRepository.GetByIdAsync(request.ImmobilienTypeId)
                ?? throw new NotFoundException(nameof(ImmobilienType), request.ImmobilienTypeId.ToString());

            overview.ImmobilienType = type;

            var hausgeld = await hausgeldRepository.GetByIdAsync(overview.ImmobilienHausgeld.Id)
                ?? throw new NotFoundException(nameof(ImmobilienHausgeld), overview.ImmobilienHausgeld.Id.ToString());

            // Recalculate values based on updated Wohnflaeche
            decimal wohnflaecheDecimal = Convert.ToDecimal(overview.Wohnflaeche);
            decimal hausgeldProMonat = hausgeld.Hausgeld.ProQuadratmeter * wohnflaecheDecimal;
            decimal umlagefaehigProMonat = (hausgeld.UmlagefaehigesHausgeld.InProzent / 100) * hausgeldProMonat;
            decimal nichtUmlagefaehigProMonat = (hausgeld.NichtUmlagefaehigesHausgeld.InProzent / 100) * hausgeldProMonat;

            // Update tracked entity directly
            hausgeld.Hausgeld = new QuadratmeterMonatJahr(
                hausgeld.Hausgeld.ProQuadratmeter,
                hausgeldProMonat,
                hausgeldProMonat * 12
            );

            hausgeld.UmlagefaehigesHausgeld = new ProzentMonatJahr(
                hausgeld.UmlagefaehigesHausgeld.InProzent,
                umlagefaehigProMonat,
                umlagefaehigProMonat * 12
            );

            hausgeld.NichtUmlagefaehigesHausgeld = new ProzentMonatJahr(
                hausgeld.NichtUmlagefaehigesHausgeld.InProzent,
                nichtUmlagefaehigProMonat,
                nichtUmlagefaehigProMonat * 12
            );

            // Save changes to both
            await hausgeldRepository.SaveChanges();
            await overviewRepository.SaveChanges();

        }
    }
}
