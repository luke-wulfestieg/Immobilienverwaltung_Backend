using BE.Application.Bruttomietrenditen.DTOs;
using MediatR;

namespace BE.Application.Bruttomietrenditen.Commands.GetBruttomietrenditeById
{
    public class GetBruttomietrenditeByIdCommand(int bruttomietrenditeId) : IRequest<BruttomietrenditeDto>
    {
        public int bruttomietrenditeId { get; } = bruttomietrenditeId;
    }
}
