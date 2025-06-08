using AutoMapper;
using BE.Application.Gesamtbelastungen.DTOs;
using BE.Domain.Entities;
using BE.Domain.Exceptions;
using BE.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BE.Application.Gesamtbelastungen.Commands.GetGesamtbelastungById
{
    public class GetGesamtbelastungByIdCommandHandler
         (ILogger<GetGesamtbelastungByIdCommandHandler> logger,
        IMapper mapper,
        IGesamtbelastungRepository gesamtbelastungRepository) : IRequestHandler<GetGesamtbelastungByIdCommand, GesamtbelastungDto>
    {
        public async Task<GesamtbelastungDto> Handle(GetGesamtbelastungByIdCommand request, CancellationToken cancellationToken)
        {
            var gesamtbelastungen =
                await gesamtbelastungRepository.GetByIdAsync(request.gesamtbelastungId) ??
                throw new NotFoundException(nameof(Gesamtbelastung), request.gesamtbelastungId.ToString());
            var gesamtbelastungenDto = mapper.Map<GesamtbelastungDto>(gesamtbelastungen);

            return gesamtbelastungenDto;
        }
    }
}
