using AutoMapper;
using BE.Application.ImmobilienHypotheken.DTOs;
using BE.Application.ImmobilienOverviews.DTOs;
using BE.Domain.Entities.Hypothek;
using BE.Domain.Exceptions;
using BE.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BE.Application.ImmobilienHypotheken.Commands.GetHypothekByOverviewId
{
    public class GetImmobilienHypothekByOverviewIdCommandHandler
        (ILogger<GetImmobilienHypothekByOverviewIdCommandHandler> logger,
        IMapper mapper,
        IImmobilienHypothekRepository hypothekRepository,
        IImmobilienOverviewRepository overviewRepository) : IRequestHandler<GetImmobilienHypothekByOverviewIdCommand, ImmobilienHypothekDto>
    {
        public async Task<ImmobilienHypothekDto> Handle(GetImmobilienHypothekByOverviewIdCommand request, CancellationToken cancellationToken)
        {
            var immobilienOverview =
               await overviewRepository.GetByIdAsync(request.overviewId) ??
               throw new NotFoundException(nameof(ImmobilienHypothek), request.overviewId.ToString());
            var immobilienOverviewDto = mapper.Map<ImmobilienOverviewDto>(immobilienOverview);

            var immobilienHypothek =
                await hypothekRepository.GetByIdAsync(immobilienOverviewDto.ImmobilienHypothek.Id) ??
                throw new NotFoundException(nameof(ImmobilienHypothek), immobilienOverviewDto.ImmobilienHypothek.Id.ToString());
            var immobilienHypothekDto = mapper.Map<ImmobilienHypothekDto>(immobilienHypothek);

            return immobilienHypothekDto;
        }
    }
}
