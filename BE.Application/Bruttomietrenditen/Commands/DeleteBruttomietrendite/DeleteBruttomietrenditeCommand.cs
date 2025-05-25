using MediatR;

namespace BE.Application.Bruttomietrenditen.Commands.DeleteBruttomietrendite
{
    public class DeleteBruttomietrenditeCommand(int id) : IRequest
    {
        public int Id { get; } = id;
    }
}
