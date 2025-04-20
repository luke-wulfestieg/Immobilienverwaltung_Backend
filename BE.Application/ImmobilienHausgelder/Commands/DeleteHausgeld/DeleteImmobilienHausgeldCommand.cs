using MediatR;

namespace BE.Application.ImmobilienHausgelder.Commands.DeleteHausgeld
{
    public class DeleteImmobilienHausgeldCommand(int id) : IRequest
    {
        public int Id { get; } = id;
    }
}
