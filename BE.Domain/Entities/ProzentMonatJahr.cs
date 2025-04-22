namespace BE.Domain.Entities
{
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
