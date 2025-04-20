using BE.Application.ImmobilienTypes.DTOs;
using MediatR;

namespace BE.Application.ImmobilienTypes.Commands.GetTypesById
{
    public class GetImmobilienTypesByIdCommand : IRequest<ImmobilienTypeDto>
    {
        public GetImmobilienTypesByIdCommand(int id)
        {
            Id = id;
        }

        public int Id { get; }
    }
}
