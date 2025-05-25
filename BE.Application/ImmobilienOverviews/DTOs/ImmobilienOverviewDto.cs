using BE.Application.ImmobilienHausgelder.DTOs;
using BE.Application.ImmobilienHypotheken.DTOs;
using BE.Application.ImmobilienTypes.DTOs;

namespace BE.Application.ImmobilienOverviews.DTOs
{
    public class ImmobilienOverviewDto
    {
        public int Id { get; set; }
        public string ImmobilienName { get; set; }
        public ImmobilienTypeDto ImmobilienType { get; set; }
        public uint Kaufpreis { get; set; }
        public decimal ZimmerAnzahl { get; set; }
        public double Wohnflaeche { get; set; }
        public decimal ImmobilienUeberschuss { get; set; }
        public ImmobilienHausgeldDto ImmobilienHausgeld { get; set; }
        public ImmobilienHypothekDto ImmobilienHypothek { get; set; }
    }
}
