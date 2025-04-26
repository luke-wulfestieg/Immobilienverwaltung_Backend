using BE.Domain.Entities.Hypothek;
using BE.Domain.Entities;
using MediatR;

namespace BE.Application.ImmobilienHypotheken.Commands.UpdateHypothek
{
    public class UpdateImmobilienHypothekByIdCommand : IRequest
    {
        public int Id { get; set; }
        public uint Kaufpreis { get; set; }
        public Kaufnebenkosten Kaufnebenkosten { get; set; }
        public ProzentBetrag Eigenkapital { get; set; }
        public decimal DarlehensBetrag { get; set; }
        public int Sollzinsbindung { get; set; }
        public Kreditbelastung Kreditbelastung { get; set; }
        public decimal Restschuld { get; set; }
    }
}
