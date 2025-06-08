using AutoMapper;
using BE.Application.ImmobilienHypotheken.Commands.CreateHypothek;
using BE.Domain.Entities.Hypothek;
using BE.Domain.Entities;
using BE.Domain.Exceptions;
using BE.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BE.Application.Gesamtbelastungen.Commands.CreateGesamtbelastung
{
    public class CreateGesamtbelastungCommandHandler
         (ILogger<CreateGesamtbelastungCommandHandler> logger,
        IMapper mapper,
        IGesamtbelastungRepository gesamtbelastungRepository,
        IImmobilienOverviewRepository overviewRepository)
    : IRequestHandler<CreateGesamtbelastungCommand, int>
    {
        public async Task<int> Handle(CreateGesamtbelastungCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Creating a new {@Gesamtbelastung}", request);

            var overview = await overviewRepository.GetByIdAsync(request.ImmobilienOverviewId) ?? throw new NotFoundException(nameof(ImmobilienOverview), request.ImmobilienOverviewId.ToString());
            var hypothek = mapper.Map<Gesamtbelastung>(request);

            return await gesamtbelastungRepository.Create(hypothek);
        }
    }
}
