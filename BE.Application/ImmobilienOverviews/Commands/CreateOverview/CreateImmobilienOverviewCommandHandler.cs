using AutoMapper;
using BE.Domain.Entities;
using BE.Domain.Entities.Hypothek;
using BE.Domain.Exceptions;
using BE.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BE.Application.ImmobilienOverviews.Commands.CreateOverview
{
    public class CreateImmobilienOverviewCommandHandler
    (ILogger<CreateImmobilienOverviewCommandHandler> logger,
    IMapper mapper,
    IImmobilienOverviewRepository overviewRepository,
    IImmobilienTypeRepository typeRepository,
    IImmobilienHausgeldRepository hausgeldRepository,
    IImmobilienHypothekRepository hypothekRepository,
    IBruttomietrenditeRepository bruttomietrenditeRepository)
    : IRequestHandler<CreateImmobilienOverviewCommand, int>
    {
        public async Task<int> Handle(CreateImmobilienOverviewCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Creating ImmobilienOverview {ImmobilienOverview}", request);
            var type = await typeRepository.GetByIdAsync(request.ImmobilienTypeId);
            
            if (type is null)
            {
                throw new NotFoundException(nameof(ImmobilienType), request.ImmobilienTypeId.ToString());
            }

            var overview = mapper.Map<ImmobilienOverview>(request);
            overview.ImmobilienType = type;
            var overviewId = await overviewRepository.Create(overview);

            await CreateDefaultHausgeld(request, overview, overviewId);
            await CreateDefaultHypothek(request, overview, overviewId);
            await CreateDefaultBruttomietrendite(request, overview, overviewId);
            return overviewId;
        }

        private async Task<int> CreateDefaultBruttomietrendite(CreateImmobilienOverviewCommand request, ImmobilienOverview overview, int overviewId)
        {
            decimal HausgeldProMonat = 3m * Convert.ToDecimal(overview.Wohnflaeche);
            decimal UmlagefaehigProMonat = 0.6m * HausgeldProMonat;

            decimal KaltmieteProMonat = Convert.ToDecimal(6 * overview.Wohnflaeche);
            decimal WarmmieteProMonat = UmlagefaehigProMonat + KaltmieteProMonat;

            decimal WarmmieteQM = WarmmieteProMonat / Convert.ToDecimal(overview.Wohnflaeche);

            var bruttomietrendite = request.Bruttomietrendite != null
                ? mapper.Map<Bruttomietrendite>(request.Bruttomietrendite)
                : new Bruttomietrendite
                {
                    Kaufpreis = overview.Kaufpreis,
                    Wohnflaeche = overview.Wohnflaeche,
                    UmlagefaehigesHausgeld = new ProzentMonatJahr(60, UmlagefaehigProMonat, (UmlagefaehigProMonat * 12)),
                    Kaltmiete = new QuadratmeterMonatJahr(Convert.ToDecimal(6), KaltmieteProMonat, KaltmieteProMonat * 12),
                    Warmmiete = new QuadratmeterMonatJahr(WarmmieteQM, WarmmieteProMonat, WarmmieteProMonat * 12),
                    KaufpreisFaktor = Convert.ToDouble(overview.Kaufpreis / (KaltmieteProMonat * 12)),
                    BruttomietrenditeBetrag = Convert.ToDouble(((KaltmieteProMonat * 12) / overview.Kaufpreis) * 100) 
                };
            bruttomietrendite.ImmobilienOverviewId = overviewId;

            return await bruttomietrenditeRepository.Create(bruttomietrendite);
        }

        private async Task<int> CreateDefaultHausgeld(CreateImmobilienOverviewCommand request, ImmobilienOverview overview, int overviewId)
        {
            decimal HausgeldProMonat = 3m * Convert.ToDecimal(overview.Wohnflaeche);
            decimal UmlagefaehigProMonat = 0.6m * HausgeldProMonat;
            decimal NichtUmlagefaehigProMonat = 0.4m * HausgeldProMonat;

            var hausgeld = request.ImmobilienHausgeld != null
                ? mapper.Map<ImmobilienHausgeld>(request.ImmobilienHausgeld)
                : new ImmobilienHausgeld
                {
                    Hausgeld = new QuadratmeterMonatJahr(3, HausgeldProMonat, (HausgeldProMonat * 12)),
                    UmlagefaehigesHausgeld = new ProzentMonatJahr(60, UmlagefaehigProMonat, (UmlagefaehigProMonat * 12)),
                    NichtUmlagefaehigesHausgeld = new ProzentMonatJahr(40, NichtUmlagefaehigProMonat, (NichtUmlagefaehigProMonat * 12))
                };
                hausgeld.ImmobilienOverviewId = overviewId;

            return await hausgeldRepository.Create(hausgeld);
        }

        private async Task<int> CreateDefaultHypothek(CreateImmobilienOverviewCommand request, ImmobilienOverview overview, int overviewId)
        {
            var kaufpreis = Convert.ToDecimal(overview.Kaufpreis);
            var kaufnebenkosten = new Kaufnebenkosten(
                new ProzentBetrag(5m, kaufpreis * 0.05m),
                new ProzentBetrag(1.5m, kaufpreis * 0.015m),
                new ProzentBetrag(0.5m, kaufpreis * 0.005m),
                new ProzentBetrag(3.57m, kaufpreis * 0.0357m),
                new ProzentBetrag(1.43m, kaufpreis * 0.0143m));

            var eigenkapital = new ProzentBetrag(25m, kaufpreis * 0.25m);
            var darlehensbetrag = kaufpreis + kaufnebenkosten.Gesamtnebenkosten.Betrag - eigenkapital.Betrag;
            var sollzinsbindung = 15;
            var kreditbelastung = new Kreditbelastung(
                new ProzentMonatJahr(4m, (darlehensbetrag * 0.04m) / 12, (darlehensbetrag * 0.04m)),
                new ProzentMonatJahr(2m, (darlehensbetrag * 0.02m) / 12, (darlehensbetrag * 0.02m)),
                new ProzentMonatJahr(0,0,0));

            var restschuld = calculateDefaultRestschuld(darlehensbetrag, sollzinsbindung, kreditbelastung);

            var hypothek = request.ImmobilienHypothek != null
                ? mapper.Map<ImmobilienHypothek>(request.ImmobilienHypothek)
                : new ImmobilienHypothek
                {
                    Kaufpreis = Convert.ToUInt32(kaufpreis),
                    Kaufnebenkosten = kaufnebenkosten,
                    Eigenkapital = eigenkapital,
                    DarlehensBetrag = darlehensbetrag,
                    Sollzinsbindung = sollzinsbindung,
                    Kreditbelastung = kreditbelastung,
                    Restschuld = restschuld
                };
            hypothek.ImmobilienOverviewId = overviewId;

            return await hypothekRepository.Create(hypothek);
        }

        private decimal calculateDefaultRestschuld(decimal darlehensBetrag, int sollzinsbindung, Kreditbelastung kreditbelastung)
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
                    restschuld = restschuld - tilgungBetragProMonat - sondertilgungBetragProJahr;
                }
                else
                {
                    restschuld = restschuld - tilgungBetragProMonat;
                }
            }
            return restschuld;
        }
    }
}
