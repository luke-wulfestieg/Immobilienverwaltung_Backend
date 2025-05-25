using BE.Domain.Entities;
using MediatR;

namespace BE.Application.Bruttomietrenditen.Commands.CreateBruttomietrendite
{
    public class CreateBruttomietrenditeCommand: IRequest<int>
    {
        public uint Kaufpreis { get; set; }

        public double Wohnflaeche { get; set; }
        
        public ProzentMonatJahr UmlagefaehigesHausgeld { get; set; }

        public QuadratmeterMonatJahr Kaltmiete { get; set; }

        public QuadratmeterMonatJahr Warmmiete { get; set; }

        public double KaufpreisFaktor { get; set; }

        public double BruttoMietrendite { get; set; }

        public int ImmobilienOverviewId { get; set; }
    }
}
