using BE.Application.Ruecklagen.DTOs;
using MediatR;

namespace BE.Application.Ruecklagen.Commands.GetAllRuecklagen
{
    public class GetAllRuecklagenCommand : IRequest<IEnumerable<RuecklagenDto>>
    {
    }
}
