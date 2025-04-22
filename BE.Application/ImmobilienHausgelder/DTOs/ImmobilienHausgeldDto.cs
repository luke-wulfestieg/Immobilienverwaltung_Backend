using BE.Domain.Entities;

namespace BE.Application.ImmobilienHausgelder.DTOs
{
    public class ImmobilienHausgeldDto
    {
        public int Id { get; set; }
        
        public QuadratmeterMonatJahr Hausgeld { get; set; }

        public ProzentMonatJahr UmlagefaehigesHausgeld { get; set; }

        public ProzentMonatJahr NichtUmlagefaehigesHausgeld { get; set; }

    }
}
