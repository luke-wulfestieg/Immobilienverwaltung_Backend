using BE.Application.ImmobilienHypotheken.DTOs;
using BE.Application.Ruecklagen.DTOs;
using MediatR;

namespace BE.Application.Ruecklagen.Commands.GeRuecklagenById
{
    public class GetRuecklagenByIdCommand(int ruecklagenId) : IRequest<RuecklagenDto>
    {
        public int ruecklagenId { get; } = ruecklagenId;

    }
}
