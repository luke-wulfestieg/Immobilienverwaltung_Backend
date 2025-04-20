using BE.Application.ImmobilienHausgelder.DTOs;
using MediatR;

namespace BE.Application.ImmobilienHausgelder.Commands.GetHausgeldById
{
    public class GetImmobilienHausgeldByIdCommand(int overviewId, int hausgeldId) : IRequest<ImmobilienHausgeldDto>
    {
        public int overviewId { get; } = overviewId;
        public int hausgeldId { get; } = hausgeldId;
    }
}
