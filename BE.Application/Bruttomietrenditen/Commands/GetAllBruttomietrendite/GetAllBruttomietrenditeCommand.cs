using BE.Application.Bruttomietrenditen.DTOs;
using MediatR;

namespace BE.Application.Bruttomietrenditen.Commands.GetAllBruttomietrendite
{
    public class GetAllBruttomietrenditeCommand : IRequest<IEnumerable<BruttomietrenditeDto>>
    {
    }
}
