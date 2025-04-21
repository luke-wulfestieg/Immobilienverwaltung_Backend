using BE.Application.ImmobilienHausgelder.DTOs;
using MediatR;

namespace BE.Application.ImmobilienHausgelder.Commands.GetHausgeldByOverviewId
{
    public class GetHausgeldByOverviewIdCommand(int overviewId) : IRequest<ImmobilienHausgeldDto>
    {
        public int overviewId { get; } = overviewId;
    }
}
