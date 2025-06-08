using BE.Application.Bruttomietrenditen.DTOs;
using BE.Application.Gesamtbelastungen.DTOs;
using MediatR;

namespace BE.Application.Gesamtbelastungen.Commands.GetAllGesamtbelastung
{
    public class GetAllGesamtbelastungCommand : IRequest<IEnumerable<GesamtbelastungDto>>
    {
    }
}
