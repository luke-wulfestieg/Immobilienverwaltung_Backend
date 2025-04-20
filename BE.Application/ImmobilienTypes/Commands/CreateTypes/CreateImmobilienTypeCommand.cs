using MediatR;

namespace BE.Application.ImmobilienTypes.Commands.CreateTypes
{
    public class CreateImmobilienTypeCommand : IRequest<int>
    {
        public string TypeName { get; set; } = default!;
    }
}
