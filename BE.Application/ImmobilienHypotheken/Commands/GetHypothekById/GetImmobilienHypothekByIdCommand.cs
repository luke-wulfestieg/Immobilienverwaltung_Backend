using BE.Application.ImmobilienHypotheken.DTOs;
using MediatR;

namespace BE.Application.ImmobilienHypotheken.Commands.GetHypothekById
{
    public class GetImmobilienHypothekByIdCommand(int hypothekId) : IRequest<ImmobilienHypothekDto>
    {
        public int hypothekId { get; } = hypothekId;

    }
}
