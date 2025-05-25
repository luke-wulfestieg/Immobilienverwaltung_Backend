using AutoMapper;
using BE.Application.Bruttomietrenditen.DTOs;
using BE.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BE.Application.Bruttomietrenditen.Commands.GetAllBruttomietrendite
{
    public class GetAllBruttomietrenditeCommandHandler
    (ILogger<GetAllBruttomietrenditeCommandHandler> logger,
    IMapper mapper,
    IBruttomietrenditeRepository bruttomietrenditeRepository) : IRequestHandler<GetAllBruttomietrenditeCommand, IEnumerable<BruttomietrenditeDto>>
    {
        public async Task<IEnumerable<BruttomietrenditeDto>> Handle(GetAllBruttomietrenditeCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Getting all Bruttomietrenditen - {Types}", request);

            var allBruttomietrendite = await bruttomietrenditeRepository.GetAllAsync();

            var allBruttomietrenditeDtos = mapper.Map<IEnumerable<BruttomietrenditeDto>>(allBruttomietrendite);

            return allBruttomietrenditeDtos!;
        }
    }
}
