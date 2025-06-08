using AutoMapper;
using BE.Domain.Entities;
using BE.Domain.Exceptions;
using BE.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BE.Application.Gesamtbelastungen.Commands.DeleteGesamtbelastung
{
    public class DeleteGesamtbelastungCommandHandler
        (ILogger<DeleteGesamtbelastungCommandHandler> logger,
        IMapper mapper,
        IGesamtbelastungRepository gesamtbelastungRepository) : IRequestHandler<DeleteGesamtbelastungCommand>
    {
        public async Task Handle(DeleteGesamtbelastungCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Deleting Gesamtbelastung with {Id}", request.Id);

            var gesamtbelastung = await gesamtbelastungRepository.GetByIdAsync(request.Id);

            if (gesamtbelastung is null)
            {
                throw new NotFoundException(nameof(Gesamtbelastung), request.Id.ToString());
            }

            await gesamtbelastungRepository.Delete(gesamtbelastung);
        }
    }
}
