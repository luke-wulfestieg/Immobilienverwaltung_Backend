using BE.Domain.Entities;

namespace BE.Application.Ruecklagen.DTOs
{
    public class RuecklagenDto
    {
        public int Id { get; set; }

        public QuadratmeterMonatJahr Instandhaltung { get; set; }

        public ProzentMonatJahr Mietausfall { get; set; }

        public MonatJahr RuecklagenBetrag { get; set; }
    }
}
