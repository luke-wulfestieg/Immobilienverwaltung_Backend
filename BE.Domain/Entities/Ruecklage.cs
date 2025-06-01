namespace BE.Domain.Entities
{
    public class Ruecklage
    {
        public int Id { get; set; }

        public QuadratmeterMonatJahr Instandhaltung { get; set; }

        public ProzentMonatJahr Mietausfall { get; set; }

        public MonatJahr RuecklagenBetrag {  get; set; }

        public int ImmobilienOverviewId { get; set; }

        public ImmobilienOverview ImmobilienOverview { get; set; }
    }
}
