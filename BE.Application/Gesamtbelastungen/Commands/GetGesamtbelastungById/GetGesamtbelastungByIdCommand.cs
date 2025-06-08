using BE.Application.Gesamtbelastungen.DTOs;
using MediatR;

namespace BE.Application.Gesamtbelastungen.Commands.GetGesamtbelastungById
{
    public class GetGesamtbelastungByIdCommand(int gesamtbelastungId) : IRequest<GesamtbelastungDto>
    {
        public int gesamtbelastungId { get; } = gesamtbelastungId;
    }
}
