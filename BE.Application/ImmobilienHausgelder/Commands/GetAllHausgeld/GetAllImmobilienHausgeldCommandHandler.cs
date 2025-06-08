using AutoMapper;
using BE.Application.ImmobilienHausgelder.DTOs;
using BE.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BE.Application.ImmobilienHausgelder.Commands.GetAllHausgeld
{
    public class GetAllImmobilienHausgeldCommandHandler
        (ILogger<GetAllImmobilienHausgeldCommandHandler> logger,
        IMapper mapper,
        IImmobilienHausgeldRepository hausgeldRepository) : IRequestHandler<GetAllImmobilienHausgeldCommand, IEnumerable<ImmobilienHausgeldDto>>
    {
        public async Task<IEnumerable<ImmobilienHausgeldDto>> Handle(GetAllImmobilienHausgeldCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Getting all Hausgelder - {Types}", request);
            
            var allHausgeld = await hausgeldRepository.GetAllAsync();

            var allHausgeldDtos = mapper.Map<IEnumerable<ImmobilienHausgeldDto>>(allHausgeld);

            return allHausgeldDtos!;
        }
    }
}
