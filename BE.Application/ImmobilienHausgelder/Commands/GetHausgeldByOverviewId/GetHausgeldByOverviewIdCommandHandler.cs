using AutoMapper;
using BE.Application.ImmobilienHausgelder.DTOs;
using BE.Application.ImmobilienOverviews.DTOs;
using BE.Domain.Entities;
using BE.Domain.Exceptions;
using BE.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BE.Application.ImmobilienHausgelder.Commands.GetHausgeldByOverviewId
{
    public class GetHausgeldByOverviewIdCommandHandler(ILogger<GetHausgeldByOverviewIdCommand> logger,
        IMapper mapper,
        IImmobilienHausgeldRepository hausgeldRepository,
        IImmobilienOverviewRepository overviewRepository) : IRequestHandler<GetHausgeldByOverviewIdCommand, ImmobilienHausgeldDto>
    {
        public async Task<ImmobilienHausgeldDto> Handle(GetHausgeldByOverviewIdCommand request, CancellationToken cancellationToken)
        {
            var immobilienOverview =
               await overviewRepository.GetByIdAsync(request.overviewId) ??
               throw new NotFoundException(nameof(ImmobilienOverview), request.overviewId.ToString());
            var immobilienOverviewDto = mapper.Map<ImmobilienOverviewDto>(immobilienOverview);

            var immobilienHausgeld =
                await hausgeldRepository.GetByIdAsync(immobilienOverviewDto.ImmobilienHausgeld.Id) ??
                throw new NotFoundException(nameof(ImmobilienHausgeld), immobilienOverviewDto.ImmobilienHausgeld.Id.ToString());
            var immobilienHausgeldDto = mapper.Map<ImmobilienHausgeldDto>(immobilienHausgeld);

            return immobilienHausgeldDto;
        }
    }
}
