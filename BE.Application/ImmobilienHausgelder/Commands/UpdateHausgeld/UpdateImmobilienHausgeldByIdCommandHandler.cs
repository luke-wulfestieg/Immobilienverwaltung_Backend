using AutoMapper;
using BE.Domain.Entities;
using BE.Domain.Exceptions;
using BE.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BE.Application.ImmobilienHausgelder.Commands.UpdateHausgeld
{
    public class UpdateImmobilienHausgeldByIdCommandHandler
        (ILogger<UpdateImmobilienHausgeldByIdCommandHandler> logger,
        IMapper mapper,
        IBruttomietrenditeRepository bruttomietrenditeRepository,
        IGesamtbelastungRepository gesamtbelastungRepository,
        IImmobilienHausgeldRepository hausgeldRepository,
        IImmobilienHypothekRepository immobilienHypothekRepository,
        IRuecklagenRepository ruecklagenRepository) : IRequestHandler<UpdateImmobilienHausgeldByIdCommand>
    {
        public async Task Handle(UpdateImmobilienHausgeldByIdCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Updating Hausgeld with ID: {Id}. New Values: HausgeldProMonat: {HausgeldProMonat}, HausgeldProJahr: {HausgeldProJahr} ...", request.Id, request.Hausgeld.ProMonat, request.Hausgeld.ProJahr);

            var hausgeld = await hausgeldRepository.GetByIdAsync(request.Id);

            if (hausgeld == null)
            {
                throw new NotFoundException(nameof(ImmobilienHausgeld), request.Id.ToString());
            }

            mapper.Map(request, hausgeld);

            var bruttomietrendite = await bruttomietrenditeRepository.GetByIdAsync(request.Id);
            if (bruttomietrendite == null)
            {
                throw new NotFoundException(nameof(Bruttomietrendite), request.Id.ToString());
            }
            bruttomietrendite.UmlagefaehigesHausgeld.InProzent = hausgeld.UmlagefaehigesHausgeld.InProzent;
            bruttomietrendite.UmlagefaehigesHausgeld.ProMonat = hausgeld.UmlagefaehigesHausgeld.ProMonat;
            bruttomietrendite.UmlagefaehigesHausgeld.ProJahr = hausgeld.UmlagefaehigesHausgeld.ProJahr;
            
            bruttomietrendite.Warmmiete.ProMonat = bruttomietrendite.Kaltmiete.ProMonat + hausgeld.UmlagefaehigesHausgeld.ProMonat;
            bruttomietrendite.Warmmiete.ProJahr = (bruttomietrendite.Kaltmiete.ProMonat + hausgeld.UmlagefaehigesHausgeld.ProMonat) * 12;
            bruttomietrendite.Warmmiete.ProQuadratmeter = bruttomietrendite.Warmmiete.ProQuadratmeter = bruttomietrendite.Warmmiete.ProMonat / Convert.ToDecimal(bruttomietrendite.Wohnflaeche);;

            var gesamtbelastung = await gesamtbelastungRepository.GetByIdAsync(request.Id);
            if (gesamtbelastung == null)
            {
                throw new NotFoundException(nameof(Gesamtbelastung), request.Id.ToString());
            }
            var hypothek = await immobilienHypothekRepository.GetByIdAsync(request.Id);
            var ruecklagen = await ruecklagenRepository.GetByIdAsync(request.Id);
            
            gesamtbelastung.Kreditbelastung = new MonatJahr(hypothek.Kreditbelastung.GesamtKreditbelastung.ProMonat, hypothek.Kreditbelastung.GesamtKreditbelastung.ProJahr);
            gesamtbelastung.Ruecklagen = new MonatJahr(ruecklagen.RuecklagenBetrag.ProMonat, ruecklagen.RuecklagenBetrag.ProJahr);
            gesamtbelastung.NichtUmlagefaehigesHausgeld = new MonatJahr(hausgeld.NichtUmlagefaehigesHausgeld.ProMonat, hausgeld.NichtUmlagefaehigesHausgeld.ProJahr);
            gesamtbelastung.GesamtbelastungBetrag = new MonatJahr((hypothek.Kreditbelastung.GesamtKreditbelastung.ProMonat + ruecklagen.RuecklagenBetrag.ProMonat + hausgeld.NichtUmlagefaehigesHausgeld.ProMonat), (hypothek.Kreditbelastung.GesamtKreditbelastung.ProJahr + ruecklagen.RuecklagenBetrag.ProJahr + hausgeld.NichtUmlagefaehigesHausgeld.ProJahr));

            await bruttomietrenditeRepository.SaveChanges();
            await gesamtbelastungRepository.SaveChanges();
            await hausgeldRepository.SaveChanges();
        }
    }
}
