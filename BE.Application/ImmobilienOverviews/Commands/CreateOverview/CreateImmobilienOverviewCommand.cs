using BE.Application.ImmobilienHausgelder.DTOs;
using MediatR;

namespace BE.Application.ImmobilienOverviews.Commands.CreateOverview
{
    public class CreateImmobilienOverviewCommand : IRequest<int>
    {
        public string ImmobilienName { get; set; } = default!;
        public uint Kaufpreis { get; set; }
        public decimal ZimmerAnzahl { get; set; }
        public double Wohnflaeche { get; set; }
        public double BruttoMietRendite { get; set; }
        public decimal ImmobilienUeberschuss { get; set; }
        public int ImmobilienTypeId { get; set; }
        public ImmobilienHausgeldDto? ImmobilienHausgeld { get; set; } = default!;
    }

}
