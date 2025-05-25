using BE.Domain.Entities;
using MediatR;

namespace BE.Application.Bruttomietrenditen.Commands.UpdateBruttomietrendite
{
    public class UpdateBruttomietrenditeByIdCommand : IRequest
    {
        public int Id { get; set; }
        public uint Kaufpreis { get; set; }

        public double Wohnflaeche { get; set; }

        public ProzentMonatJahr UmlagefaehigesHausgeld { get; set; }

        public QuadratmeterMonatJahr Kaltmiete { get; set; }

        public QuadratmeterMonatJahr Warmmiete { get; set; }

        public double KaufpreisFaktor { get; set; }

        public double BruttomietrenditeBetrag { get; set; }
    }
}
