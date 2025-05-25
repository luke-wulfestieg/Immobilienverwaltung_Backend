using BE.Application.Bruttomietrenditen.DTOs;
using MediatR;

namespace BE.Application.Bruttomietrenditen.Commands.GetBruttomietrenditeByOverviewId
{
    public class GetBruttomietrenditeByOverviewIdCommand(int overviewId) : IRequest<BruttomietrenditeDto>
    {
        public int overviewId { get; } = overviewId;
    }
}
