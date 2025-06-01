using AutoMapper;
using BE.Domain.Entities;
using BE.Domain.Exceptions;
using BE.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BE.Application.Ruecklagen.Commands.CreateRuecklagen
{
    public class CreateRuecklagenCommandHandler(ILogger<CreateRuecklagenCommandHandler> logger,
    IMapper mapper,
    IRuecklagenRepository ruecklagenRepository,
    IImmobilienOverviewRepository overviewRepository)
    : IRequestHandler<CreateRuecklagenCommand, int>
    {
        public async Task<int> Handle(CreateRuecklagenCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Creating a new {@RuecklageRequest}", request);

            var overview = await overviewRepository.GetByIdAsync(request.ImmobilienOverviewId) ?? throw new NotFoundException(nameof(Ruecklage), request.ImmobilienOverviewId.ToString());
            var ruecklage = mapper.Map<Ruecklage>(request);

            return await ruecklagenRepository.Create(ruecklage);
        }
    }
}
