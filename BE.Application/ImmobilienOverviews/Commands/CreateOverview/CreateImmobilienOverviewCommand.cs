using BE.Application.Bruttomietrenditen.DTOs;
using BE.Application.Gesamtbelastungen.DTOs;
using BE.Application.ImmobilienHausgelder.DTOs;
using BE.Application.ImmobilienHypotheken.DTOs;
using BE.Application.Ruecklagen.DTOs;
using MediatR;

namespace BE.Application.ImmobilienOverviews.Commands.CreateOverview
{
    public class CreateImmobilienOverviewCommand : IRequest<int>
    {
        public string ImmobilienName { get; set; } = default!;
        public uint Kaufpreis { get; set; }
        public decimal ZimmerAnzahl { get; set; }
        public double Wohnflaeche { get; set; }
        public decimal ImmobilienUeberschuss { get; set; }
        public int ImmobilienTypeId { get; set; }
        public ImmobilienHausgeldDto? ImmobilienHausgeld { get; set; } = default!;
        public ImmobilienHypothekDto? ImmobilienHypothek { get; set; } = default!;
        public BruttomietrenditeDto? Bruttomietrendite { get; set; } = default!;
        public RuecklagenDto? Ruecklage {  get; set; } = default!;
        public GesamtbelastungDto? Gesamtbelastung { get; set; } = default!;
    }

}
