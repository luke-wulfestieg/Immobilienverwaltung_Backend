using BE.Domain.Entities;
using MediatR;

namespace BE.Application.Gesamtbelastungen.Commands.UpdateGesamtbelastung
{
    public class UpdateGesamtbelastungCommand : IRequest
    {
        public int Id { get; set; }

        public MonatJahr Kreditbelastung { get; set; }

        public MonatJahr Ruecklagen { get; set; }

        public MonatJahr NichtUmlagefaehigesHausgeld { get; set; }

        public MonatJahr GesamtbelastungBetrag { get; set; }
    }
}
