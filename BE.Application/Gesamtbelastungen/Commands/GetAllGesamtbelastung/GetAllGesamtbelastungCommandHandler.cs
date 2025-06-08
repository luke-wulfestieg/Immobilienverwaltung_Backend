using AutoMapper;
using BE.Application.Gesamtbelastungen.DTOs;
using BE.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BE.Application.Gesamtbelastungen.Commands.GetAllGesamtbelastung
{
    public class GetAllGesamtbelastungCommandHandler
          (ILogger<GetAllGesamtbelastungCommandHandler> logger,
        IMapper mapper,
        IGesamtbelastungRepository gesamtbelastungRepository) : IRequestHandler<GetAllGesamtbelastungCommand, IEnumerable<GesamtbelastungDto>>
    {
        public async Task<IEnumerable<GesamtbelastungDto>> Handle(GetAllGesamtbelastungCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Getting all Gesamtbelastungen - {Types}", request);

            var allGesamtbelastung = await gesamtbelastungRepository.GetAllAsync();

            var allGesamtbelastungDtos = mapper.Map<IEnumerable<GesamtbelastungDto>>(allGesamtbelastung);

            return allGesamtbelastungDtos!;
        }
    }
}
