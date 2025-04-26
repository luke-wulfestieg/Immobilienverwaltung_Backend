using AutoMapper;
using BE.Domain.Entities;
using BE.Domain.Entities.Hypothek;
using BE.Domain.Exceptions;
using BE.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BE.Application.ImmobilienHypotheken.Commands.CreateHypothek
{
    public class CreateImmobilienHypothekCommandHandler
    (ILogger<CreateImmobilienHypothekCommandHandler> logger,
    IMapper mapper,
    IImmobilienHypothekRepository hypothekenRepository,
    IImmobilienOverviewRepository overviewRepository)
    : IRequestHandler<CreateImmobilienHypothekCommand, int>
    {
        public async Task<int> Handle(CreateImmobilienHypothekCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Creating a new {@HypothekRequest}", request);

            var overview = await overviewRepository.GetByIdAsync(request.ImmobilienOverviewId) ?? throw new NotFoundException(nameof(ImmobilienOverview), request.ImmobilienOverviewId.ToString());
            var hypothek = mapper.Map<ImmobilienHypothek>(request);

            return await hypothekenRepository.Create(hypothek);
        }
    }
}
