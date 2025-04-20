using AutoMapper;
using BE.Application.ImmobilienHausgelder.DTOs;
using BE.Application.ImmobilienTypes.Commands.GetTypesById;
using BE.Application.ImmobilienTypes.DTOs;
using BE.Domain.Entities;
using BE.Domain.Exceptions;
using BE.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BE.Application.ImmobilienHausgelder.Commands.GetHausgeldById
{
    public class GetImmobilienHausgeldByIdCommandHandler
        (ILogger<GetImmobilienHausgeldByIdCommandHandler> logger,
        IMapper mapper,
        IImmobilienHausgeldRepository hausgeldRepository) : IRequestHandler<GetImmobilienHausgeldByIdCommand, ImmobilienHausgeldDto>
    {
        public async Task<ImmobilienHausgeldDto> Handle(GetImmobilienHausgeldByIdCommand request, CancellationToken cancellationToken)
        {
            var immobilienHausgeld =
                await hausgeldRepository.GetByIdAsync(request.hausgeldId) ??
                throw new NotFoundException(nameof(ImmobilienHausgeld), request.hausgeldId.ToString());
            var immobilienHausgeldDto = mapper.Map<ImmobilienHausgeldDto>(immobilienHausgeld);

            return immobilienHausgeldDto;
        }
    }
}
