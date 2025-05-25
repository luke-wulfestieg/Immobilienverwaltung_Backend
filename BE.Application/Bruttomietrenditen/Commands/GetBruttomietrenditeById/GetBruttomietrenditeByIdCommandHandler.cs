using AutoMapper;
using BE.Application.Bruttomietrenditen.DTOs;
using BE.Domain.Entities;
using BE.Domain.Exceptions;
using BE.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BE.Application.Bruttomietrenditen.Commands.GetBruttomietrenditeById
{
    public class GetBruttomietrenditeByIdCommandHandler
        (ILogger<GetBruttomietrenditeByIdCommandHandler> logger,
        IMapper mapper,
        IBruttomietrenditeRepository bruttomietrenditeRepository) : IRequestHandler<GetBruttomietrenditeByIdCommand, BruttomietrenditeDto>
    {
        public async Task<BruttomietrenditeDto> Handle(GetBruttomietrenditeByIdCommand request, CancellationToken cancellationToken)
        {
            var bruttomietrendite =
                await bruttomietrenditeRepository.GetByIdAsync(request.bruttomietrenditeId) ??
                throw new NotFoundException(nameof(Bruttomietrendite), request.bruttomietrenditeId.ToString());
            var bruttomietrenditeDto = mapper.Map<BruttomietrenditeDto>(bruttomietrendite);

            return bruttomietrenditeDto;
        }
    }
}
