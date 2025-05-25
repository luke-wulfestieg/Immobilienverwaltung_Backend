using AutoMapper;
using BE.Domain.Entities;
using BE.Domain.Exceptions;
using BE.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BE.Application.Bruttomietrenditen.Commands.UpdateBruttomietrendite
{
    internal class UpdateBruttomietrenditeByIdCommandHandler
         (ILogger<UpdateBruttomietrenditeByIdCommandHandler> logger,
        IMapper mapper,
        IBruttomietrenditeRepository bruttomietrenditeRepository) : IRequestHandler<UpdateBruttomietrenditeByIdCommand>
    {
        public async Task Handle(UpdateBruttomietrenditeByIdCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Updating Hausgeld with ID: {Id}. New Values: Kaltmiete: {Kaltmiete}, Warmmiete: {Warmmiete}, BruttoMietrendite: {Bruttomietrendite} ...", request.Id, request.Kaltmiete, request.Warmmiete, request.BruttomietrenditeBetrag);

            var bruttomietrendite = await bruttomietrenditeRepository.GetByIdAsync(request.Id);

            if (bruttomietrendite == null)
            {
                throw new NotFoundException(nameof(Bruttomietrendite), request.Id.ToString());
            }

            mapper.Map(request, bruttomietrendite);

            await bruttomietrenditeRepository.SaveChanges();
        }
    }
}
