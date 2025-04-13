namespace Immobilienverwaltung_Backend.Features.Immobilien_Overview.Models
{
    public class Immobilien_Type
    {
        public int Id { get; set; }
        public string ImmobilienType { get; set; }
        public ICollection<ImmobilienOverview> ImmobilienOverviews { get; set; }
    }
}
