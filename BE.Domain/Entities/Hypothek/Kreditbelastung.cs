namespace BE.Domain.Entities.Hypothek
{
    public class Kreditbelastung
    {
        public Kreditbelastung(ProzentMonatJahr zinsen, ProzentMonatJahr tilgung, ProzentMonatJahr sonderTilgung)
        {
            Zinsen = zinsen;
            Tilgung = tilgung;
            Sondertilgung = sonderTilgung;
            GesamtKreditbelastung = new ProzentMonatJahr(
                (zinsen.InProzent + tilgung.InProzent), 
                (zinsen.ProMonat + tilgung.ProMonat), 
                (zinsen.ProJahr + tilgung.ProJahr));
        }

        public Kreditbelastung(){}

        public ProzentMonatJahr Zinsen { get; set; }
        public ProzentMonatJahr Tilgung { get; set; }
        public ProzentMonatJahr Sondertilgung { get; set; }
        public ProzentMonatJahr GesamtKreditbelastung { get; set; }
    }
}
