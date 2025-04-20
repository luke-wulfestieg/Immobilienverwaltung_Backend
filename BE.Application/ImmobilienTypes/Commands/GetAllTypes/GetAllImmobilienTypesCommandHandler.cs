using AutoMapper;
using BE.Application.ImmobilienTypes.DTOs;
using BE.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BE.Application.ImmobilienTypes.Commands.GetAllTypes
{
    public class GetAllImmobilienTypesCommandHandler
        (ILogger<GetAllImmobilienTypesCommandHandler> logger,
        IMapper mapper,
        IImmobilienTypeRepository typeRepository) : IRequestHandler<GetAllImmobilienTypesCommand, IEnumerable<ImmobilienTypeDto>>
    {
        public async Task<IEnumerable<ImmobilienTypeDto>> Handle(GetAllImmobilienTypesCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Getting all ImmobilienTypes - {Types}", request);
            var allTypes = await typeRepository.GetAllAsync();

            var allTypesDtos = mapper.Map<IEnumerable<ImmobilienTypeDto>>(allTypes);

            return allTypesDtos!;
        }
    }
}
