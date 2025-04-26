using MediatR;

namespace BE.Application.ImmobilienHypotheken.Commands.DeleteHypothek
{
    public class DeleteImmobilienHypothekByIdCommand(int id) : IRequest
    {
        public int Id { get; } = id;
    }
}
