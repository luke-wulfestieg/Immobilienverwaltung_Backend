namespace BE.Domain.Entities
{
    public class QuadratmeterMonatJahr
    {
        public QuadratmeterMonatJahr(decimal proQuadratmeter, decimal proMonat, decimal proJahr)
        {
            ProQuadratmeter = proQuadratmeter;
            ProMonat = proMonat;    
            ProJahr = proJahr;
        }
        public decimal ProQuadratmeter { get; set; }
        public decimal ProMonat { get; set; }
        public decimal ProJahr { get; set; }
    }
}
