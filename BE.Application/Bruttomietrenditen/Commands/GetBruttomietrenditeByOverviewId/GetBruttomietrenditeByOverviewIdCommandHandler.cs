using AutoMapper;
using BE.Application.Bruttomietrenditen.DTOs;
using BE.Application.ImmobilienOverviews.DTOs;
using BE.Domain.Entities;
using BE.Domain.Exceptions;
using BE.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BE.Application.Bruttomietrenditen.Commands.GetBruttomietrenditeByOverviewId
{
    public class GetBruttomietrenditeByOverviewIdCommandHandler
        (ILogger<GetBruttomietrenditeByOverviewIdCommandHandler> logger,
        IMapper mapper,
        IBruttomietrenditeRepository bruttomietrenditeRepository,
        IImmobilienOverviewRepository overviewRepository) : IRequestHandler<GetBruttomietrenditeByOverviewIdCommand, BruttomietrenditeDto>
    {
        public async Task<BruttomietrenditeDto> Handle(GetBruttomietrenditeByOverviewIdCommand request, CancellationToken cancellationToken)
        {
            var immobilienOverview =
              await overviewRepository.GetByIdAsync(request.overviewId) ??
              throw new NotFoundException(nameof(ImmobilienOverview), request.overviewId.ToString());
            var immobilienOverviewDto = mapper.Map<ImmobilienOverviewDto>(immobilienOverview);

            var bruttomietrendite =
                await bruttomietrenditeRepository.GetByIdAsync(immobilienOverviewDto.Bruttomietrendite.Id) ??
                throw new NotFoundException(nameof(Bruttomietrendite), immobilienOverviewDto.Bruttomietrendite.Id.ToString());
            var bruttomietrenditeDto = mapper.Map<BruttomietrenditeDto>(bruttomietrendite);

            return bruttomietrenditeDto;
        }
    }
}
