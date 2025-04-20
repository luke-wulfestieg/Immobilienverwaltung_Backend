using BE.Application.ImmobilienTypes.DTOs;
using MediatR;

namespace BE.Application.ImmobilienTypes.Commands.GetAllTypes
{
    public class GetAllImmobilienTypesCommand : IRequest<IEnumerable<ImmobilienTypeDto>>
    {
    }
}
