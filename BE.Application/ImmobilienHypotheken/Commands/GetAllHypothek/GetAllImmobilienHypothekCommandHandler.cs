using AutoMapper;
using BE.Application.ImmobilienHypotheken.DTOs;
using BE.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BE.Application.ImmobilienHypotheken.Commands.GetAllHypothek
{
    public class GetAllImmobilienHypothekCommandHandler
         (ILogger<GetAllImmobilienHypothekCommandHandler> logger,
        IMapper mapper,
        IImmobilienHypothekRepository hypothekRepository)
        : IRequestHandler<GetAllImmobilienHypothekCommand, IEnumerable<ImmobilienHypothekDto>>
    {
        public async Task<IEnumerable<ImmobilienHypothekDto>> Handle(GetAllImmobilienHypothekCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Getting all Hypotheken - {Types}", request);

            var allHypothek = await hypothekRepository.GetAllAsync();

            var allHypothekDtos = mapper.Map<IEnumerable<ImmobilienHypothekDto>>(allHypothek);

            return allHypothekDtos!;
        }
    }
}
