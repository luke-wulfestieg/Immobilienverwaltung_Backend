using AutoMapper;
using BE.Application.ImmobilienTypes.Commands.UpdateTypes;
using BE.Domain.Entities;
using BE.Domain.Exceptions;
using BE.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BE.Application.ImmobilienHausgelder.Commands.UpdateHausgeld
{
    public class UpdateImmobilienHausgeldByIdCommandHandler
        (ILogger<UpdateImmobilienHausgeldByIdCommandHandler> logger,
        IMapper mapper,
        IImmobilienHausgeldRepository hausgeldRepository) : IRequestHandler<UpdateImmobilienHausgeldByIdCommand>
    {
        public async Task Handle(UpdateImmobilienHausgeldByIdCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Updating Hausgeld with ID: {Id}. New Values: HausgeldProMonat: {HausgeldProMonat}, HausgeldProJahr: {HausgeldProJahr} ...", request.Id, request.HausgeldProMonat, request.HausgeldProJahr);

            var hausgeld = await hausgeldRepository.GetByIdAsync(request.Id);

            if (hausgeld == null)
            {
                throw new NotFoundException(nameof(ImmobilienHausgeld), request.Id.ToString());
            }

            mapper.Map(request, hausgeld);

            await hausgeldRepository.SaveChanges();
        }
    }
}
