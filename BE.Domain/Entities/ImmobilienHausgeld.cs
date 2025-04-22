namespace BE.Domain.Entities
{
    public class ImmobilienHausgeld
    {
        public int Id { get; set; }

        public QuadratmeterMonatJahr Hausgeld { get; set; }

        public ProzentMonatJahr UmlagefaehigesHausgeld { get; set; }

        public ProzentMonatJahr NichtUmlagefaehigesHausgeld { get; set; }
        public int ImmobilienOverviewId { get; set; }
        public ImmobilienOverview ImmobilienOverview { get; set; }


    }
}
