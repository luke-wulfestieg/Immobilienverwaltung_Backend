using AutoMapper;
using BE.Domain.Entities;
using BE.Domain.Exceptions;
using BE.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BE.Application.Bruttomietrenditen.Commands.DeleteBruttomietrendite
{
    public class DeleteBruttomietrenditeCommandHandler
    (ILogger<DeleteBruttomietrenditeCommandHandler> logger,
    IMapper mapper,
    IBruttomietrenditeRepository bruttomietrenditeRepository) : IRequestHandler<DeleteBruttomietrenditeCommand>
    {
        public async Task Handle(DeleteBruttomietrenditeCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Deleting Bruttomietrendite with {Id}", request.Id);

            var bruttomietrendite = await bruttomietrenditeRepository.GetByIdAsync(request.Id);

            if (bruttomietrendite is null)
            {
                throw new NotFoundException(nameof(Bruttomietrendite), request.Id.ToString());
            }

            await bruttomietrenditeRepository.Delete(bruttomietrendite);
        }
    }
}
