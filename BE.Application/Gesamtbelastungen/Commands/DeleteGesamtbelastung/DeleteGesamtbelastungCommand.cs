using MediatR;

namespace BE.Application.Gesamtbelastungen.Commands.DeleteGesamtbelastung
{
    public class DeleteGesamtbelastungCommand(int id) : IRequest
    {
        public int Id { get; } = id;
    }
}
