using BE.Application.ImmobilienHausgelder.DTOs;
using BE.Domain.Entities;
using MediatR;

namespace BE.Application.ImmobilienHausgelder.Commands.GetAllHausgeld
{
    public class GetAllImmobilienHausgeldCommand() : IRequest<IEnumerable<ImmobilienHausgeldDto>>
    {
    }
}
