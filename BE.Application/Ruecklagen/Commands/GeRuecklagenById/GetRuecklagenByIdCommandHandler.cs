using AutoMapper;
using BE.Application.ImmobilienHypotheken.Commands.GetHypothekById;
using BE.Application.ImmobilienHypotheken.DTOs;
using BE.Application.Ruecklagen.DTOs;
using BE.Domain.Entities;
using BE.Domain.Entities.Hypothek;
using BE.Domain.Exceptions;
using BE.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BE.Application.Ruecklagen.Commands.GeRuecklagenById
{
    public class GetRuecklagenByIdCommandHandler
     (ILogger<GetRuecklagenByIdCommandHandler> logger,
        IMapper mapper,
        IRuecklagenRepository ruecklagenRepository) : IRequestHandler<GetRuecklagenByIdCommand, RuecklagenDto>
    {
        public async Task<RuecklagenDto> Handle(GetRuecklagenByIdCommand request, CancellationToken cancellationToken)
        {
            var ruecklage =
                await ruecklagenRepository.GetByIdAsync(request.ruecklagenId) ??
                throw new NotFoundException(nameof(Ruecklage), request.ruecklagenId.ToString());
            var ruecklageDto = mapper.Map<RuecklagenDto>(ruecklage);

            return ruecklageDto;
        }
    }
}
