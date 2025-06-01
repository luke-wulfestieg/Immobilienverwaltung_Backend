using AutoMapper;
using BE.Application.Ruecklagen.DTOs;
using BE.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BE.Application.Ruecklagen.Commands.GetAllRuecklagen
{
    public class GetAllRuecklagenCommandHandler(
        ILogger<GetAllRuecklagenCommandHandler> logger,
        IMapper mapper,
        IRuecklagenRepository ruecklagenRepository)
        : IRequestHandler<GetAllRuecklagenCommand, IEnumerable<RuecklagenDto>>
    {
        public async Task<IEnumerable<RuecklagenDto>> Handle(GetAllRuecklagenCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Getting all Ruecklagen - {@Request}", request);

            var allRuecklagen = await ruecklagenRepository.GetAllAsync();

            var allRuecklagenDtos = mapper.Map<IEnumerable<RuecklagenDto>>(allRuecklagen);

            return allRuecklagenDtos!;
        }
    }
}
