﻿using AutoMapper;
using BE.Domain.Entities;
using BE.Domain.Entities.Hypothek;
using BE.Domain.Exceptions;
using BE.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BE.Application.ImmobilienOverviews.Commands.UpdateOverviewById
{
    public class UpdateImmobilienOverviewCommandHandler(
            ILogger<UpdateImmobilienOverviewCommandHandler> logger,
            IMapper mapper,
            IImmobilienOverviewRepository overviewRepository,
            IImmobilienTypeRepository typeRepository,
            IImmobilienHausgeldRepository hausgeldRepository,
            IImmobilienHypothekRepository hypothekRepository)
        : IRequestHandler<UpdateImmobilienOverviewCommand>
    {
        public async Task Handle(UpdateImmobilienOverviewCommand request, CancellationToken cancellationToken)
        {
            var overview = await overviewRepository.GetByIdAsync(request.Id)
                ?? throw new NotFoundException(nameof(ImmobilienOverview), request.Id.ToString());

            mapper.Map(request, overview);

            var type = await typeRepository.GetByIdAsync(request.ImmobilienTypeId)
                ?? throw new NotFoundException(nameof(ImmobilienType), request.ImmobilienTypeId.ToString());

            overview.ImmobilienType = type;

            var hausgeld = await hausgeldRepository.GetByIdAsync(overview.ImmobilienHausgeld.Id)
                ?? throw new NotFoundException(nameof(ImmobilienHausgeld), overview.ImmobilienHausgeld.Id.ToString());

            // Recalculate values based on updated Wohnflaeche
            decimal wohnflaecheDecimal = Convert.ToDecimal(overview.Wohnflaeche);
            decimal hausgeldProMonat = hausgeld.Hausgeld.ProQuadratmeter * wohnflaecheDecimal;
            decimal umlagefaehigProMonat = (hausgeld.UmlagefaehigesHausgeld.InProzent / 100) * hausgeldProMonat;
            decimal nichtUmlagefaehigProMonat = (hausgeld.NichtUmlagefaehigesHausgeld.InProzent / 100) * hausgeldProMonat;

            // Update tracked entity directly
            hausgeld.Hausgeld = new QuadratmeterMonatJahr(
                hausgeld.Hausgeld.ProQuadratmeter,
                hausgeldProMonat,
                hausgeldProMonat * 12
            );

            hausgeld.UmlagefaehigesHausgeld = new ProzentMonatJahr(
                hausgeld.UmlagefaehigesHausgeld.InProzent,
                umlagefaehigProMonat,
                umlagefaehigProMonat * 12
            );

            hausgeld.NichtUmlagefaehigesHausgeld = new ProzentMonatJahr(
                hausgeld.NichtUmlagefaehigesHausgeld.InProzent,
                nichtUmlagefaehigProMonat,
                nichtUmlagefaehigProMonat * 12
            );

            var hypothek = await hypothekRepository.GetByIdAsync(overview.ImmobilienHypothek.Id)
                ?? throw new NotFoundException(nameof(ImmobilienHypothek), overview.ImmobilienHypothek.Id.ToString());

            // Pass real object
            hypothek = UpdateHypothek(overview, hypothek);


            // Save changes to both
            await hypothekRepository.SaveChanges();
            await hausgeldRepository.SaveChanges();
            await overviewRepository.SaveChanges();

        }

        private ImmobilienHypothek UpdateHypothek(ImmobilienOverview overview, ImmobilienHypothek hypothek)
        {
            var kaufpreis = Convert.ToDecimal(overview.Kaufpreis);
            var nebenkosten = hypothek.Kaufnebenkosten;

            var kaufnebenkosten = new Kaufnebenkosten(
                new ProzentBetrag(nebenkosten.GrunderwerbSteuer.InProzent, kaufpreis * (nebenkosten.GrunderwerbSteuer.InProzent / 100)),
                new ProzentBetrag(nebenkosten.Notarkosten.InProzent, kaufpreis * (nebenkosten.Notarkosten.InProzent / 100)),
                new ProzentBetrag(nebenkosten.Grundbucheintrag.InProzent, kaufpreis * (nebenkosten.Grundbucheintrag.InProzent / 100)),
                new ProzentBetrag(nebenkosten.Maklerprovision.InProzent, kaufpreis * (nebenkosten.Maklerprovision.InProzent / 100)),
                new ProzentBetrag(nebenkosten.Sicherheitspuffer.InProzent, kaufpreis * (nebenkosten.Sicherheitspuffer.InProzent / 100))
            );

            var eigenkapital = new ProzentBetrag(hypothek.Eigenkapital.InProzent, kaufpreis * (hypothek.Eigenkapital.InProzent / 100));
            var darlehensbetrag = kaufpreis + kaufnebenkosten.Gesamtnebenkosten.Betrag - eigenkapital.Betrag;
            var sollzinsbindung = hypothek.Sollzinsbindung;
            var kreditbelastung = new Kreditbelastung(
                new ProzentMonatJahr(hypothek.Kreditbelastung.Zinsen.InProzent, (darlehensbetrag * (hypothek.Kreditbelastung.Zinsen.InProzent / 100)) / 12, (darlehensbetrag * (hypothek.Kreditbelastung.Zinsen.InProzent / 100))),
                new ProzentMonatJahr(hypothek.Kreditbelastung.Tilgung.InProzent, (darlehensbetrag * (hypothek.Kreditbelastung.Tilgung.InProzent / 100)) / 12, (darlehensbetrag * ((hypothek.Kreditbelastung.Tilgung.InProzent / 100)))),
                new ProzentMonatJahr(hypothek.Kreditbelastung.Sondertilgung.InProzent, (darlehensbetrag * (hypothek.Kreditbelastung.Sondertilgung.InProzent / 100)) / 12, (darlehensbetrag * ((hypothek.Kreditbelastung.Sondertilgung.InProzent / 100))))
            );

            var restschuld = CalculateRestschuld(darlehensbetrag, sollzinsbindung, kreditbelastung);

            // Update existing tracked entity
            hypothek.Kaufpreis = Convert.ToUInt32(kaufpreis);
            hypothek.Kaufnebenkosten = kaufnebenkosten;
            hypothek.Eigenkapital = eigenkapital;
            hypothek.DarlehensBetrag = darlehensbetrag;
            hypothek.Sollzinsbindung = sollzinsbindung;
            hypothek.Kreditbelastung = kreditbelastung;
            hypothek.Restschuld = restschuld;
            hypothek.ImmobilienOverviewId = overview.Id;

            return hypothek;
        }

        private decimal CalculateRestschuld(decimal darlehensBetrag, int sollzinsbindung, Kreditbelastung kreditbelastung)
        {
            var restschuld = darlehensBetrag;
            var kreditbelastungBetragProMonat = kreditbelastung.GesamtKreditbelastung.ProMonat;
            var tilgungBetragProMonat = kreditbelastung.Tilgung.ProMonat;
            var zinsenProzent = kreditbelastung.Zinsen.InProzent;
            var zinsenBetragProMonat = kreditbelastung.Zinsen.ProMonat;
            var sondertilgungBetragProJahr = kreditbelastung.Sondertilgung.ProJahr;
            var sollzinsbindungMonate = sollzinsbindung * 12;

            for (var m = 1; m <= sollzinsbindungMonate; m++)
            {
                zinsenBetragProMonat = ((zinsenProzent / 100) * restschuld) / 12;
                tilgungBetragProMonat = kreditbelastungBetragProMonat - zinsenBetragProMonat;

                if (m % 12 == 0)
                {
                    restschuld -= tilgungBetragProMonat + sondertilgungBetragProJahr;
                }
                else
                {
                    restschuld -= tilgungBetragProMonat;
                }
            }
            return restschuld;
        }
    }
}
