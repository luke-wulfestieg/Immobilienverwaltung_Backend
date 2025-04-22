using BE.Domain.Entities;
using MediatR;

namespace BE.Application.ImmobilienHausgelder.Commands.CreateHausgeld
{
    public class CreateImmobilienHausgeldCommand : IRequest<int>
    {
        public QuadratmeterMonatJahr Hausgeld { get; set; }

        public ProzentMonatJahr UmlagefaehigesHausgeld { get; set; }

        public ProzentMonatJahr NichtUmlagefaehigesHausgeld { get; set; }

        public int ImmobilienOverviewId { get; set; }
    }
}
