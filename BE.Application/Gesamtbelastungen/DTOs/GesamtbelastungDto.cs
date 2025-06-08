using BE.Domain.Entities;

namespace BE.Application.Gesamtbelastungen.DTOs
{
    public class GesamtbelastungDto
    {
        public int Id { get; set; }

        public MonatJahr Kreditbelastung { get; set; }

        public MonatJahr Ruecklagen { get; set; }

        public MonatJahr NichtUmlagefaehigesHausgeld { get; set; }

        public MonatJahr GesamtbelastungBetrag { get; set; }
    }
}
