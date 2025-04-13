namespace Immobilienverwaltung_Backend.Features.Immobilien_Overview.Models
{
    public class ImmobilienOverview
    {
        public int Id { get; set; }
        public string ImmobilienName { get; set; }
        public int ImmobilienTypeId { get; set; }
        public Immobilien_Type ImmobilienType { get; set; }
        public uint Kaufpreis { get; set; }
        public decimal ZimmerAnzahl { get; set; }
        public double Wohnflaeche { get; set; }
        public double BruttoMietRendite { get; set; }
        public decimal ImmobilienUeberschuss { get; set; }
        public int? ImmobilienHausgeldId { get; set; }  
        public Immobilien_Hausgeld ImmobilienHausgeld { get; set; }
    }
}
