using BE.Domain.Entities;
using MediatR;

namespace BE.Application.Ruecklagen.Commands.UpdateRuecklagen
{
    public class UpdateRuecklagenCommand : IRequest
    {
        public int Id { get; set; }

        public QuadratmeterMonatJahr Instandhaltung { get; set; }

        public ProzentMonatJahr Mietausfall { get; set; }

        public MonatJahr RuecklagenBetrag { get; set; }
    }
}
