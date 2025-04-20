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

        // Constructor with dependencies injected
        public UpdateImmobilienOverviewCommandHandler(
            ILogger<UpdateImmobilienOverviewCommandHandler> logger,
            IMapper mapper,
            IImmobilienOverviewRepository overviewRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _overviewRepository = overviewRepository;
        }

        public async Task Handle(UpdateImmobilienOverviewCommand request, CancellationToken cancellationToken)
        {
            // Retrieve the existing ImmobilienOverview by ID
            var overview = await _overviewRepository.GetByIdAsync(request.Id);

            // If the ImmobilienOverview doesn't exist, throw a NotFoundException
            if (overview == null)
            {
                throw new NotFoundException(nameof(ImmobilienOverview), request.Id.ToString());
            }

            // Map the updated values from the request to the existing overview entity
            _mapper.Map(request, overview);

            // Save the updated overview to the repository
            await _overviewRepository.SaveChanges();
        }
    }
}
