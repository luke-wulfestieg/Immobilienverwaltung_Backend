using Immobilienverwaltung_Backend.Models;

namespace Immobilienverwaltung_Backend.Features.Immobilien_Overview.Models
{
    public class Immobilien_Hausgeld
    {
        public int Id { get; set; }
        public QuadratmeterMonatJahr Hausgeld { get; set; }
        public ProzentMonatJahr Umlagefaehiges_Hausgeld { get; set; }
        public ProzentMonatJahr Nicht_Umlagefaehiges_Hausgeld { get; set; }
        public int ImmobilienOverviewId { get; set; }
        public ImmobilienOverview ImmobilienOverview { get; set; }
    }
}
