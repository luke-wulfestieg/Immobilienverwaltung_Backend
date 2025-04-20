using MediatR;

namespace BE.Application.ImmobilienHausgelder.Commands.CreateHausgeld
{
    public class CreateImmobilienHausgeldCommand : IRequest<int>
    {
        public decimal HausgeldProQuadratmeter { get; set; }
        public decimal HausgeldProMonat { get; set; }
        public decimal HausgeldProJahr { get; set; }

        public decimal UmlagefaehigesHausgeldInProzent { get; set; }
        public decimal UmlagefaehigesHausgeldProMonat { get; set; }
        public decimal UmlagefaehigesHausgeldProJahr { get; set; }

        public decimal NichtUmlagefaehigesHausgeldInProzent { get; set; }
        public decimal NichtUmlagefaehigesHausgeldProMonat { get; set; }
        public decimal NichtUmlagefaehigesHausgeldProJahr { get; set; }

        public int ImmobilienOverviewId { get; set; }
    }
}
