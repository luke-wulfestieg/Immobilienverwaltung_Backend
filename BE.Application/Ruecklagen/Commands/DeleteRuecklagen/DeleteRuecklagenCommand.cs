using MediatR;

namespace BE.Application.Ruecklagen.Commands.DeleteRuecklagen
{
    public class DeleteRuecklagenCommand(int id) : IRequest
    {
        public int Id { get; } = id;

    }
}
