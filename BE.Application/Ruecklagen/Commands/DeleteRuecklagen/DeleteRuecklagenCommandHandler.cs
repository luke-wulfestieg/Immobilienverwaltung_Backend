using BE.Domain.Entities;
using BE.Domain.Exceptions;
using BE.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BE.Application.Ruecklagen.Commands.DeleteRuecklagen
{
    public class DeleteRuecklagenCommandHandler
        (ILogger<DeleteRuecklagenCommandHandler> logger,
        IRuecklagenRepository ruecklagenRepository)
    : IRequestHandler<DeleteRuecklagenCommand>
    {
        public async Task Handle(DeleteRuecklagenCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Deleting Ruecklagen with {Id}", request.Id);


            var ruecklage = await ruecklagenRepository.GetByIdAsync(request.Id);

            if (ruecklage is null)
            {
                throw new NotFoundException(nameof(Ruecklage), request.Id.ToString());
            }

            await ruecklagenRepository.Delete(ruecklage);
        }
    }
}
