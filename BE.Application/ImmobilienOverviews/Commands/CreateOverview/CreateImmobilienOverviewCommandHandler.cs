using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

            // Check if ImmobilienType exists
            var type = await typeRepository.GetByIdAsync(request.ImmobilienTypeId);
            if (type == null)
            {
                throw new NotFoundException(nameof(ImmobilienType), request.ImmobilienTypeId.ToString());
            }

            // Check if ImmobilienHausgeld exists
            var hausgeld = await hausgeldRepository.GetByIdAsync(request.ImmobilienHausgeldId);
            if (hausgeld == null)
            {
                throw new NotFoundException(nameof(ImmobilienHausgeld), request.ImmobilienHausgeldId.ToString());
            }

            // Map the command to the entity
            var overview = mapper.Map<ImmobilienOverview>(request);

            // Set the ImmobilienType and Hausgeld as navigation properties
            overview.ImmobilienType = type;
            overview.ImmobilienHausgeld = hausgeld;

            // Save the new entity
            return await overviewRepository.Create(overview);
        }
    }
}
