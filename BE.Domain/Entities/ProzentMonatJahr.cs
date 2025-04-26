using Microsoft.EntityFrameworkCore;

namespace BE.Domain.Entities
{
    [Owned]
    public class ProzentMonatJahr
    {
        public ProzentMonatJahr(decimal inProzent, decimal proMonat, decimal proJahr)
        {
            InProzent = inProzent;
            ProMonat = proMonat;
            ProJahr = proJahr;
        }
        public decimal InProzent { get; set; }
        public decimal ProMonat { get; set; }
        public decimal ProJahr { get; set; }

    }
}
