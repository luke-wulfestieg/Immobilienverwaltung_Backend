using BE.Domain.Entities;
using MediatR;

namespace BE.Application.Ruecklagen.Commands.CreateRuecklagen
{
    public class CreateRuecklagenCommand : IRequest<int>
    {

        public QuadratmeterMonatJahr Instandhaltung { get; set; }

        public ProzentMonatJahr Mietausfall { get; set; }

        public MonatJahr RuecklagenBetrag { get; set; }

        public int ImmobilienOverviewId { get; set; }
    }
}
