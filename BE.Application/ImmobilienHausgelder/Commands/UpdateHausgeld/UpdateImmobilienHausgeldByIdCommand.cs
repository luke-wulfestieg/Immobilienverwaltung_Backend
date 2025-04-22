using BE.Domain.Entities;
using MediatR;

namespace BE.Application.ImmobilienHausgelder.Commands.UpdateHausgeld
{
    public class UpdateImmobilienHausgeldByIdCommand : IRequest
    {
        public int Id { get; set; }
        public QuadratmeterMonatJahr Hausgeld { get; set; }
        public ProzentMonatJahr UmlagefaehigesHausgeld { get; set; }
        public ProzentMonatJahr NichtUmlagefaehigesHausgeld { get; set; }
    }
}
