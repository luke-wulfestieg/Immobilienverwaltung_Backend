using BE.Application.Ruecklagen.DTOs;
using MediatR;

namespace BE.Application.Ruecklagen.Commands.GetRuecklagenyOverviewId
{
    public class GetRuecklagenByOverviewIdCommand(int overviewId) : IRequest<RuecklagenDto>
    {
        public int overviewId { get; } = overviewId;
    }
}
