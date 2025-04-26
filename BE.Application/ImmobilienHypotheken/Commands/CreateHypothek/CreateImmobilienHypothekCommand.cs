using BE.Domain.Entities.Hypothek;
using BE.Domain.Entities;
using MediatR;

namespace BE.Application.ImmobilienHypotheken.Commands.CreateHypothek
{
    public class CreateImmobilienHypothekCommand : IRequest<int>
    {
        public uint Kaufpreis { get; set; }
        public Kaufnebenkosten Kaufnebenkosten { get; set; }
        public ProzentBetrag Eigenkapital { get; set; }
        public decimal DarlehensBetrag { get; set; }
        public int Sollzinsbindung { get; set; }
        public Kreditbelastung Kreditbelastung { get; set; }
        public decimal Restschuld { get; set; }
        public int ImmobilienOverviewId { get; set; }
    }
}
