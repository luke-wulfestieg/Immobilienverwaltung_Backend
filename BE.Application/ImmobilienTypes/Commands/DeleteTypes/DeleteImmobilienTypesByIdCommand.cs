using MediatR;

namespace BE.Application.ImmobilienTypes.Commands.DeleteTypes
{
    public class DeleteImmobilienTypesByIdCommand(int id) : IRequest
    {
        public int Id { get; } = id;
    }
}
