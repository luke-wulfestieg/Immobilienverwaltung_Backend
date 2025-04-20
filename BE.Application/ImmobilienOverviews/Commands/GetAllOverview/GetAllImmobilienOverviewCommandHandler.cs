using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BE.Application.ImmobilienOverviews.DTOs;
using BE.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BE.Application.ImmobilienOverviews.Commands.GetAllOverview
{
    public class GetAllImmobilienOverviewCommandHandler
        (ILogger<GetAllImmobilienOverviewCommandHandler> logger,
        IMapper mapper,
        IImmobilienOverviewRepository overviewRepository) : IRequestHandler<GetAllImmobilienOverviewCommand, IEnumerable<ImmobilienOverviewDto>>
    {
        public async Task<IEnumerable<ImmobilienOverviewDto>> Handle(GetAllImmobilienOverviewCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Getting all ImmobilienOverviews - {Overview}", request);
            var allOverviews = await overviewRepository.GetAllAsync();
            var allOverviewsDtos = mapper.Map<IEnumerable<ImmobilienOverviewDto>>(allOverviews);

            return allOverviewsDtos!;
        }
    }
}
