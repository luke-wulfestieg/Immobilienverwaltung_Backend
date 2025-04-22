using AutoMapper;
using BE.Domain.Entities;
using BE.Domain.Exceptions;
using BE.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BE.Application.ImmobilienOverviews.Commands.CreateOverview
{
    public class CreateImmobilienOverviewCommandHandler
    (ILogger<CreateImmobilienOverviewCommandHandler> logger,
    IMapper mapper,
    IImmobilienOverviewRepository overviewRepository,
    IImmobilienTypeRepository typeRepository,
    IImmobilienHausgeldRepository hausgeldRepository)
    : IRequestHandler<CreateImmobilienOverviewCommand, int>
    {
        public async Task<int> Handle(CreateImmobilienOverviewCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Creating ImmobilienOverview {ImmobilienOverview}", request);
            var type = await typeRepository.GetByIdAsync(request.ImmobilienTypeId);
            
            if (type is null)
            {
                throw new NotFoundException(nameof(ImmobilienType), request.ImmobilienTypeId.ToString());
            }

            var overview = mapper.Map<ImmobilienOverview>(request);
            overview.ImmobilienType = type;
            var overviewId = await overviewRepository.Create(overview);

            decimal HausgeldProMonat = 3m * Convert.ToDecimal(overview.Wohnflaeche);
            decimal UmlagefaehigProMonat = 0.6m * HausgeldProMonat;
            decimal NichtUmlagefaehigProMonat = 0.4m * HausgeldProMonat;

            var hausgeld = request.ImmobilienHausgeld != null
                ? mapper.Map<ImmobilienHausgeld>(request.ImmobilienHausgeld)
                : new ImmobilienHausgeld
                {
                    Hausgeld = new QuadratmeterMonatJahr(3, HausgeldProMonat, (HausgeldProMonat * 12)),
                    UmlagefaehigesHausgeld = new ProzentMonatJahr(60, UmlagefaehigProMonat, (UmlagefaehigProMonat * 12)),
                    NichtUmlagefaehigesHausgeld = new ProzentMonatJahr(40, NichtUmlagefaehigProMonat, (NichtUmlagefaehigProMonat * 12))
                };
            hausgeld.ImmobilienOverviewId = overviewId;
            await hausgeldRepository.Create(hausgeld);

            return overviewId;
        }
    }
}
