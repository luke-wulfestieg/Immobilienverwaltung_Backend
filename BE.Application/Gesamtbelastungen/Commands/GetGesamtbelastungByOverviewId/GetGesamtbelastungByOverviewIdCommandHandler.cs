using AutoMapper;
using BE.Application.Gesamtbelastungen.DTOs;
using BE.Application.ImmobilienOverviews.DTOs;
using BE.Domain.Entities;
using BE.Domain.Exceptions;
using BE.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BE.Application.Gesamtbelastungen.Commands.GetGesamtbelastungByOverviewId
{
    public class GetGesamtbelastungByOverviewIdCommandHandler
        (ILogger<GetGesamtbelastungByOverviewIdCommandHandler> logger,
        IMapper mapper,
        IGesamtbelastungRepository gesamtbelastungRepository,
        IImmobilienOverviewRepository overviewRepository) : IRequestHandler<GetGesamtbelastungByOverviewIdCommand, GesamtbelastungDto>
    {
        public async Task<GesamtbelastungDto> Handle(GetGesamtbelastungByOverviewIdCommand request, CancellationToken cancellationToken)
        {
            var immobilienOverview =
               await overviewRepository.GetByIdAsync(request.overviewId) ??
               throw new NotFoundException(nameof(ImmobilienOverview), request.overviewId.ToString());
            var immobilienOverviewDto = mapper.Map<ImmobilienOverviewDto>(immobilienOverview);

            var gesamtbelastungen =
                await gesamtbelastungRepository.GetByIdAsync(immobilienOverviewDto.Gesamtbelastung.Id) ??
                throw new NotFoundException(nameof(Gesamtbelastung), immobilienOverviewDto.Gesamtbelastung.Id.ToString());
            var gesamtbelastungenDto = mapper.Map<GesamtbelastungDto>(gesamtbelastungen);

            return gesamtbelastungenDto;
        }
    }
}
