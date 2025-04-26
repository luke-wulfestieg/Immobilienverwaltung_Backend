using BE.Application.ImmobilienHypotheken.DTOs;
using MediatR;

namespace BE.Application.ImmobilienHypotheken.Commands.GetAllHypothek
{
    public class GetAllImmobilienHypothekCommand : IRequest<IEnumerable<ImmobilienHypothekDto>>
    {
    }
}
