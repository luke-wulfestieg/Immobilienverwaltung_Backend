namespace BE.Domain.Entities
{
    public class ProzentBetrag
    {
        public ProzentBetrag(decimal inProzent, decimal betrag)
        {
            InProzent = inProzent;
            Betrag = betrag;
        }
        public decimal InProzent { get; set; }
        public decimal Betrag { get; set; }
    }
}
