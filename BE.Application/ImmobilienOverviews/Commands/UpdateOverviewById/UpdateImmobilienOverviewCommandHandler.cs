using AutoMapper;
using BE.Domain.Entities;
using BE.Domain.Exceptions;
using BE.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BE.Application.ImmobilienOverviews.Commands.UpdateOverviewById
{
    public class UpdateImmobilienOverviewCommandHandler
       : IRequestHandler<UpdateImmobilienOverviewCommand>
    {
        private readonly ILogger<UpdateImmobilienOverviewCommandHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IImmobilienOverviewRepository _overviewRepository;
        private readonly IImmobilienTypeRepository _typeRepository;

        public UpdateImmobilienOverviewCommandHandler(
            ILogger<UpdateImmobilienOverviewCommandHandler> logger,
            IMapper mapper,
            IImmobilienOverviewRepository overviewRepository, 
            IImmobilienTypeRepository typeRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _overviewRepository = overviewRepository;
            _typeRepository = typeRepository;
        }

        public async Task Handle(UpdateImmobilienOverviewCommand request, CancellationToken cancellationToken)
        {
            var overview = await _overviewRepository.GetByIdAsync(request.Id)
                ?? throw new NotFoundException(nameof(ImmobilienOverview), request.Id.ToString());

            _mapper.Map(request, overview);

            var type = await _typeRepository.GetByIdAsync(request.ImmobilienTypeId)
                ?? throw new NotFoundException(nameof(ImmobilienType), request.ImmobilienTypeId.ToString());

            overview.ImmobilienType = type;

            await _overviewRepository.SaveChanges();
        }
    }
}
