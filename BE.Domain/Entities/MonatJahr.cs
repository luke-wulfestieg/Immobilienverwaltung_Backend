using Microsoft.EntityFrameworkCore;

namespace BE.Domain.Entities
{
    [Owned]
    public class MonatJahr
    {
        public MonatJahr(decimal proMonat, decimal proJahr)
        {
            ProMonat = proMonat;
            ProJahr = proJahr;
        }
        public decimal ProMonat { get; set; }
        public decimal ProJahr { get; set; }

    }
}
