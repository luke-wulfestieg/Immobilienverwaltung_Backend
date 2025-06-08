using AutoMapper;
using BE.Domain.Entities;
using BE.Domain.Exceptions;
using BE.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BE.Application.Ruecklagen.Commands.UpdateRuecklagen
{
    public class UpdateRuecklagenCommandHandler(ILogger<UpdateRuecklagenCommandHandler> logger,
        IMapper mapper,
        IImmobilienOverviewRepository overviewRepository,
        IBruttomietrenditeRepository bruttomietrenditeRepository,
        IRuecklagenRepository ruecklagenRepository,
        IGesamtbelastungRepository gesamtbelastungRepository, 
        IImmobilienHausgeldRepository hausgeldRepository,
        IImmobilienHypothekRepository hypothekRepository) : IRequestHandler<UpdateRuecklagenCommand>
    {
        public async Task Handle(UpdateRuecklagenCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Updating Hypothek with ID: {Id}.", request.Id);

            var ruecklage = await ruecklagenRepository.GetByIdAsync(request.Id);
            var bruttomietrendite = await bruttomietrenditeRepository.GetByIdAsync(request.Id);
            var overview = await overviewRepository.GetByIdAsync(request.Id);

            if (ruecklage == null || overview == null || bruttomietrendite == null)
            {
                throw new NotFoundException(nameof(Ruecklage), request.Id.ToString());
            }

            mapper.Map(request, ruecklage);
            var kaltmieteProQm = bruttomietrendite.Kaltmiete.ProQuadratmeter;
            var kaltmieteProMonat = kaltmieteProQm * Convert.ToDecimal(overview.Wohnflaeche);
            var instandhaltung = new QuadratmeterMonatJahr(ruecklage.Instandhaltung.ProQuadratmeter, ruecklage.Instandhaltung.ProQuadratmeter * Convert.ToDecimal(overview.Wohnflaeche), (ruecklage.Instandhaltung.ProQuadratmeter * Convert.ToDecimal(overview.Wohnflaeche)) * 12);
            var mietausfall = new ProzentMonatJahr(ruecklage.Mietausfall.InProzent, kaltmieteProMonat * (ruecklage.Mietausfall.InProzent / 100), (kaltmieteProMonat * 12) * (ruecklage.Mietausfall.InProzent / 100));
            var ruecklagen = new MonatJahr(instandhaltung.ProMonat + mietausfall.ProMonat, instandhaltung.ProJahr + mietausfall.ProJahr);



            ruecklage.Instandhaltung = instandhaltung;
            ruecklage.Mietausfall = mietausfall;
            ruecklage.RuecklagenBetrag = ruecklagen;

            var gesamtbelastung = await gesamtbelastungRepository.GetByIdAsync(request.Id);
            if (gesamtbelastung == null)
            {
                throw new NotFoundException(nameof(Gesamtbelastung), request.Id.ToString());
            }
            var hypothek = await hypothekRepository.GetByIdAsync(request.Id);
            var hausgeld = await hausgeldRepository.GetByIdAsync(request.Id);
            gesamtbelastung.Kreditbelastung = new MonatJahr(hypothek.Kreditbelastung.GesamtKreditbelastung.ProMonat, hypothek.Kreditbelastung.GesamtKreditbelastung.ProJahr);
            gesamtbelastung.Ruecklagen = new MonatJahr(ruecklage.RuecklagenBetrag.ProMonat, ruecklage.RuecklagenBetrag.ProJahr);
            gesamtbelastung.NichtUmlagefaehigesHausgeld = new MonatJahr(hausgeld.NichtUmlagefaehigesHausgeld.ProMonat, hausgeld.NichtUmlagefaehigesHausgeld.ProJahr);
            gesamtbelastung.GesamtbelastungBetrag = new MonatJahr((hypothek.Kreditbelastung.GesamtKreditbelastung.ProMonat + ruecklage.RuecklagenBetrag.ProMonat + hausgeld.NichtUmlagefaehigesHausgeld.ProMonat), (hypothek.Kreditbelastung.GesamtKreditbelastung.ProJahr + ruecklage.RuecklagenBetrag.ProJahr + hausgeld.NichtUmlagefaehigesHausgeld.ProJahr));


            await gesamtbelastungRepository.SaveChanges();
            await ruecklagenRepository.SaveChanges();
        }
    }
}
