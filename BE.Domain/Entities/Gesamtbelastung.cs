namespace BE.Domain.Entities
{
    public class Gesamtbelastung
    {
        public int Id { get; set; }

        public MonatJahr Kreditbelastung { get; set; }

        public MonatJahr Ruecklagen {  get; set; }

        public MonatJahr NichtUmlagefaehigesHausgeld {  get; set; }

        public MonatJahr GesamtbelastungBetrag {  get; set; }

        public int ImmobilienOverviewId { get; set; }

        public ImmobilienOverview ImmobilienOverview { get; set; }
    }
}
