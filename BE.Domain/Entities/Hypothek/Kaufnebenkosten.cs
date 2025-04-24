namespace BE.Domain.Entities.Hypothek
{
    public class Kaufnebenkosten
    {
        public Kaufnebenkosten(ProzentBetrag grunderwerb, ProzentBetrag notar, ProzentBetrag grundbuch, ProzentBetrag makler, ProzentBetrag sicherheitspuffer)
        {
            GrunderwerbSteuer = grunderwerb;
            Notarkosten = notar;
            Grundbucheintrag = grundbuch;
            Maklerprovision = makler;
            Sicherheitspuffer = sicherheitspuffer;
            Gesamtnebenkosten = new ProzentBetrag
                ((grunderwerb.InProzent +
                notar.InProzent +
                grundbuch.InProzent +
                makler.InProzent +
                sicherheitspuffer.InProzent), (grunderwerb.Betrag +
                notar.Betrag +
                grundbuch.Betrag +
                makler.Betrag +
                sicherheitspuffer.Betrag));
        }

        protected Kaufnebenkosten() { }

        public ProzentBetrag GrunderwerbSteuer { get; set; }

        public ProzentBetrag Notarkosten { get; set; }

        public ProzentBetrag Grundbucheintrag { get; set; }

        public ProzentBetrag Maklerprovision { get; set; }

        public ProzentBetrag Sicherheitspuffer { get; set; }

        public ProzentBetrag Gesamtnebenkosten { get; set; }
    }
}
