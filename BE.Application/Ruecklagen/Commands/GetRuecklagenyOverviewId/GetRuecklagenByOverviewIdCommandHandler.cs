using AutoMapper;
using BE.Application.ImmobilienOverviews.DTOs;
using BE.Application.Ruecklagen.DTOs;
using BE.Domain.Entities;
using BE.Domain.Exceptions;
using BE.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BE.Application.Ruecklagen.Commands.GetRuecklagenyOverviewId
{
    public class GetRuecklagenByOverviewIdCommandHandler(
        ILogger<GetRuecklagenByOverviewIdCommandHandler> logger,
        IMapper mapper,
        IRuecklagenRepository ruecklagenRepository,
        IImmobilienOverviewRepository overviewRepository
    ) : IRequestHandler<GetRuecklagenByOverviewIdCommand, RuecklagenDto>
    {
        public async Task<RuecklagenDto> Handle(GetRuecklagenByOverviewIdCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Fetching Ruecklage for Overview ID: {OverviewId}", request.overviewId);


            var immobilienOverview = await overviewRepository.GetByIdAsync(request.overviewId)
                ?? throw new NotFoundException(nameof(ImmobilienOverview), request.overviewId.ToString());
            var immobilienOverviewDto = mapper.Map<ImmobilienOverviewDto>(immobilienOverview);

            // Ensure Ruecklage is available on the entity directly before mapping
            if (immobilienOverview.Ruecklage == null)
            {
                throw new NotFoundException(nameof(Ruecklage), $"Ruecklage not set on ImmobilienOverview with ID {request.overviewId}");
            }

            var ruecklageEntity = await ruecklagenRepository.GetByIdAsync(immobilienOverviewDto.Ruecklage.Id)
                ?? throw new NotFoundException(nameof(Ruecklage), immobilienOverviewDto.Ruecklage.Id.ToString());

            return mapper.Map<RuecklagenDto>(ruecklageEntity);
        }
    }
}
