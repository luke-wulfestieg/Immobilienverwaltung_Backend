using AutoMapper;
using BE.Application.ImmobilienTypes.DTOs;
using BE.Domain.Entities;
using BE.Domain.Exceptions;
using BE.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BE.Application.ImmobilienTypes.Commands.GetTypesById
{
    public class GetImmobilienTypesByIdCommandHandler
        (ILogger<GetImmobilienTypesByIdCommandHandler> logger,
        IMapper mapper,
        IImmobilienTypeRepository typeRepository) : IRequestHandler<GetImmobilienTypesByIdCommand, ImmobilienTypeDto>
    {
        public async Task<ImmobilienTypeDto> Handle(GetImmobilienTypesByIdCommand request, CancellationToken cancellationToken)
        {
            var immobilienType =
                await typeRepository.GetByIdAsync(request.Id) ??
                throw new NotFoundException(nameof(ImmobilienType), request.Id.ToString());
            var immobilienTypeDto = mapper.Map<ImmobilienTypeDto>(immobilienType);

            return immobilienTypeDto;
        }
    }
}
