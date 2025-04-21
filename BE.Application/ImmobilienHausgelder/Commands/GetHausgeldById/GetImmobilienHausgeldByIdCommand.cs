using BE.Application.ImmobilienHausgelder.DTOs;
using MediatR;

namespace BE.Application.ImmobilienHausgelder.Commands.GetHausgeldById
{
    public class GetImmobilienHausgeldByIdCommand(int hausgeldId) : IRequest<ImmobilienHausgeldDto>
    {
        public int hausgeldId { get; } = hausgeldId;
    }
}
