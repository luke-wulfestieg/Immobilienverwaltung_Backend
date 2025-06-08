using AutoMapper;
using BE.Domain.Entities;
using BE.Domain.Exceptions;
using BE.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BE.Application.Bruttomietrenditen.Commands.CreateBruttomietrendite
{
    public class CreateBruttomietrenditeCommandHandler
        (ILogger<CreateBruttomietrenditeCommandHandler> logger,
        IMapper mapper,
        IBruttomietrenditeRepository bruttomietrenditenRepository,
        IImmobilienOverviewRepository overviewRepository)
        : IRequestHandler<CreateBruttomietrenditeCommand, int>
    {
        public async Task<int> Handle(CreateBruttomietrenditeCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Creating a new {@BruttomietrenditeRequest}", request);

            var overview = await overviewRepository.GetByIdAsync(request.ImmobilienOverviewId);

            if (overview is null)
            {
                throw new NotFoundException(nameof(Bruttomietrendite), request.ImmobilienOverviewId.ToString());
            }
            var bruttomietrendite = mapper.Map<Bruttomietrendite>(request);

            return await bruttomietrenditenRepository.Create(bruttomietrendite);
        }
    }
}
