using BE.Domain.Entities;
using MediatR;

namespace BE.Application.Gesamtbelastungen.Commands.CreateGesamtbelastung
{
    public class CreateGesamtbelastungCommand : IRequest<int>
    {
        public MonatJahr Kreditbelastung { get; set; }

        public MonatJahr Ruecklagen { get; set; }

        public MonatJahr NichtUmlagefaehigesHausgeld { get; set; }

        public MonatJahr GesamtbelastungBetrag { get; set; }
        public int ImmobilienOverviewId { get; set; }
    }
}
