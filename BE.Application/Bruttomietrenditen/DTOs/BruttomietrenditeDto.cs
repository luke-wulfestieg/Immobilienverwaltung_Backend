using BE.Domain.Entities;

namespace BE.Application.Bruttomietrenditen.DTOs
{
    public class BruttomietrenditeDto
    {
        public int Id { get; set; }

        public uint Kaufpreis { get; set; }
        
        public double Wohnflaeche { get; set; }
        
        public ProzentMonatJahr UmlagefaehigesHausgeld { get; set; }

        public QuadratmeterMonatJahr Kaltmiete { get; set; }

        public QuadratmeterMonatJahr Warmmiete { get; set; }

        public double KaufpreisFaktor { get; set; }

        public double BruttoMietrendite { get; set; }
    }
}
