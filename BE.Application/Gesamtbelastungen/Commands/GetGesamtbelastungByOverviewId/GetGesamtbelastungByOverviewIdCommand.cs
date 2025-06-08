using BE.Application.Bruttomietrenditen.DTOs;
using BE.Application.Gesamtbelastungen.DTOs;
using MediatR;

namespace BE.Application.Gesamtbelastungen.Commands.GetGesamtbelastungByOverviewId
{
    public class GetGesamtbelastungByOverviewIdCommand(int overviewId) : IRequest<GesamtbelastungDto>
    {
        public int overviewId { get; } = overviewId;
    }
}
