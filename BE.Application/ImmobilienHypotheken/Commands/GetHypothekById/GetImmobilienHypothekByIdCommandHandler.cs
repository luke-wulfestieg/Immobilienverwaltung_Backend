using AutoMapper;
using BE.Application.ImmobilienHypotheken.DTOs;
using BE.Domain.Entities.Hypothek;
using BE.Domain.Exceptions;
using BE.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BE.Application.ImmobilienHypotheken.Commands.GetHypothekById
{
    public class GetImmobilienHypothekByIdCommandHandler
        (ILogger<GetImmobilienHypothekByIdCommandHandler> logger,
        IMapper mapper,
        IImmobilienHypothekRepository hypothekRepository) : IRequestHandler<GetImmobilienHypothekByIdCommand, ImmobilienHypothekDto>
    {
        public async Task<ImmobilienHypothekDto> Handle(GetImmobilienHypothekByIdCommand request, CancellationToken cancellationToken)
        {
            var immobilienHypothek =
                await hypothekRepository.GetByIdAsync(request.hypothekId) ??
                throw new NotFoundException(nameof(ImmobilienHypothek), request.hypothekId.ToString());
            var immobilienHypohekDto = mapper.Map<ImmobilienHypothekDto>(immobilienHypothek);

            return immobilienHypohekDto;
        }
    }
}
