namespace BE.Domain.Entities.Hypothek
{
    public class ImmobilienHypothek
    {
        public int Id { get; set; }
        public uint Kaufpreis { get; set; }
        public Kaufnebenkosten Kaufnebenkosten { get; set; }
        public ProzentBetrag Eigenkapital { get; set; }
        public decimal DarlehensBetrag { get; set; }
        public int Sollzinsbindung { get; set; }
        public Kreditbelastung Kreditbelastung { get; set; }
        public decimal Restschuld { get; set; }
        public int ImmobilienOverviewId { get; set; }
        public ImmobilienOverview ImmobilienOverview { get; set; }
    }
}
