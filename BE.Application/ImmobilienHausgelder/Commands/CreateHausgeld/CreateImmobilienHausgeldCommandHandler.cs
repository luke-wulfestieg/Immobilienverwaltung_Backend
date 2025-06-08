using AutoMapper;
using BE.Domain.Entities;
using BE.Domain.Exceptions;
using BE.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BE.Application.ImmobilienHausgelder.Commands.CreateHausgeld
{
    public class CreateImmobilienHausgeldCommandHandler
        (ILogger<CreateImmobilienHausgeldCommandHandler> logger,
        IMapper mapper,
        IImmobilienHausgeldRepository hausgeldRepository,
        IImmobilienOverviewRepository overviewRepository)
        : IRequestHandler<CreateImmobilienHausgeldCommand, int>
    {
        public async Task<int> Handle(CreateImmobilienHausgeldCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Creating a new {@HausgeldRequest}", request);

            var overview = await overviewRepository.GetByIdAsync(request.ImmobilienOverviewId);

            if (overview is null)
            {
                throw new NotFoundException(nameof(ImmobilienHausgeld), request.ImmobilienOverviewId.ToString());
            }
            var hausgeld = mapper.Map<ImmobilienHausgeld>(request);

            return await hausgeldRepository.Create(hausgeld);
        }
    }
}
