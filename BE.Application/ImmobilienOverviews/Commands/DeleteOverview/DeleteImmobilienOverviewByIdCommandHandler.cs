using BE.Domain.Entities;
using BE.Domain.Exceptions;
using BE.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BE.Application.ImmobilienOverviews.Commands.DeleteOverview
{
    public class DeleteImmobilienOverviewByIdCommandHandler
    (ILogger<DeleteImmobilienOverviewByIdCommandHandler> logger,
     IImmobilienOverviewRepository overviewRepository)
    : IRequestHandler<DeleteImmobilienOverviewByIdCommand>
    {
        public async Task Handle(DeleteImmobilienOverviewByIdCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Deleting ImmobilienOverview with {Id}", request.Id);

            var overview = await overviewRepository.GetByIdAsync(request.Id);

            if (overview is null)
            {
                throw new NotFoundException(nameof(ImmobilienOverview), request.Id.ToString());
            }

            await overviewRepository.Delete(overview);
            // Ensure changes are saved
            await overviewRepository.SaveChanges();
        }
    }
}
