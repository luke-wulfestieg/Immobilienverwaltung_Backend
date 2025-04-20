using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BE.Application.ImmobilienOverviews.DTOs;
using BE.Domain.Entities;
using BE.Domain.Exceptions;
using BE.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BE.Application.ImmobilienOverviews.Commands.GetOverviewById
{
    public class GetImmobilienOverviewByIdCommandHandler
    (ILogger<GetImmobilienOverviewByIdCommandHandler> logger,
    IMapper mapper,
    IImmobilienOverviewRepository overviewRepository) : IRequestHandler<GetImmobilienOverviewByIdCommand, ImmobilienOverviewDto>
    {
        public async Task<ImmobilienOverviewDto> Handle(GetImmobilienOverviewByIdCommand request, CancellationToken cancellationToken)
        {
            var immobilienOverview =
                await overviewRepository.GetByIdAsync(request.Id) ??
                throw new NotFoundException(nameof(ImmobilienOverview), request.Id.ToString());

            var immobilienOverviewDto = mapper.Map<ImmobilienOverviewDto>(immobilienOverview);

            return immobilienOverviewDto;
        }
    }

}
