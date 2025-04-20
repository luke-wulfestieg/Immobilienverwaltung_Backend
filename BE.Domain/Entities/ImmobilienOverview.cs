namespace BE.Domain.Entities
{
    public class ImmobilienOverview
    {
        public int Id { get; set; }
        public string ImmobilienName { get; set; }
        public ImmobilienType ImmobilienType { get; set; }
        public uint Kaufpreis { get; set; }
        public decimal ZimmerAnzahl { get; set; }
        public double Wohnflaeche { get; set; }
        public double BruttoMietRendite { get; set; }
        public decimal ImmobilienUeberschuss { get; set; }
        public ImmobilienHausgeld ImmobilienHausgeld { get; set; }
    }
}
