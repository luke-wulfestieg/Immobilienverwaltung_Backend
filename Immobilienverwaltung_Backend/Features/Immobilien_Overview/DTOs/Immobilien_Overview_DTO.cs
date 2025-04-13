using Immobilienverwaltung_Backend.Features.Immobilien_Overview.Models;

namespace Immobilienverwaltung_Backend.Features.Immobilien_Overview.DTOs
{
    public class Immobilien_Overview_DTO
    {
        public int Id { get; set; }
        public string ImmobilienName { get; set; }
        public Immobilien_Type_DTO ImmobilienType { get; set; }
        public decimal Kaufpreis { get; set; }
        public int ZimmerAnzahl { get; set; }
        public double Wohnflaeche { get; set; }
        public double BruttoMietRendite { get; set; }
        public decimal ImmobilienUeberschuss { get; set; }
        public Immobilien_Hausgeld_DTO? ImmobilienHausgeld { get; set; }
    }
}
