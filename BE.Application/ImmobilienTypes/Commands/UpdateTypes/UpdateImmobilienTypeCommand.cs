using MediatR;

namespace BE.Application.ImmobilienTypes.Commands.UpdateTypes
{
    public class UpdateImmobilienTypeCommand : IRequest
    {
        public int Id { get; set; }
        public string TypeName { get; set; }
    }
}
