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

            var hausgeld = request.ImmobilienHausgeld != null
                ? mapper.Map<ImmobilienHausgeld>(request.ImmobilienHausgeld)
                : new ImmobilienHausgeld
                {
                    HausgeldProQuadratmeter = 3,
                    HausgeldProMonat = 0,
                    HausgeldProJahr = 0,
                    UmlagefaehigesHausgeldInProzent = 60,
                    UmlagefaehigesHausgeldProMonat = 0,
                    UmlagefaehigesHausgeldProJahr = 0,
                    NichtUmlagefaehigesHausgeldInProzent = 40,
                    NichtUmlagefaehigesHausgeldProMonat = 0,
                    NichtUmlagefaehigesHausgeldProJahr = 0,
                };
            hausgeld.ImmobilienOverviewId = overviewId;
            await hausgeldRepository.Create(hausgeld);

            return overviewId;
        }
    }
}
