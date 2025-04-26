using BE.Application.ImmobilienHypotheken.DTOs;
using MediatR;

namespace BE.Application.ImmobilienHypotheken.Commands.GetHypothekByOverviewId
{
    public class GetImmobilienHypothekByOverviewIdCommand(int overviewId) : IRequest<ImmobilienHypothekDto>
    {
        public int overviewId { get; } = overviewId;
    }
}
