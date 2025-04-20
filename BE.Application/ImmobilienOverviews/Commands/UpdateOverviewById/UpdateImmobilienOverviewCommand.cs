using MediatR;

namespace BE.Application.ImmobilienOverviews.Commands.UpdateOverviewById
{
    public class UpdateImmobilienOverviewCommand : IRequest
    {
        public int Id { get; set; }
        public string ImmobilienName { get; set; }
        public int ImmobilienTypeId { get; set; }
        public uint Kaufpreis { get; set; }
        public decimal ZimmerAnzahl { get; set; }
        public double Wohnflaeche { get; set; }
        public double BruttoMietRendite { get; set; }
        public decimal ImmobilienUeberschuss { get; set; }
    }
}
