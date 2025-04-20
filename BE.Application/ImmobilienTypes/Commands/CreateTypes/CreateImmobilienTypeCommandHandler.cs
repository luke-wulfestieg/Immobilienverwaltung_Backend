using AutoMapper;
using BE.Domain.Entities;
using BE.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BE.Application.ImmobilienTypes.Commands.CreateTypes
{
    public class CreateImmobilienTypeCommandHandler
        (ILogger<CreateImmobilienTypeCommandHandler> logger,
        IMapper mapper,
        IImmobilienTypeRepository typeRepository)
        : IRequestHandler<CreateImmobilienTypeCommand, int>
    {
        public async Task<int> Handle(CreateImmobilienTypeCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Creating ImmobilienType {ImmobilienType}", request);

            var type = mapper.Map<ImmobilienType>(request);

            int id = await typeRepository.Create(type);

            return id;
        }
    }
}
