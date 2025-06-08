using AutoMapper;
using BE.Domain.Entities;
using BE.Domain.Entities.Hypothek;
using BE.Domain.Exceptions;
using BE.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BE.Application.ImmobilienHypotheken.Commands.UpdateHypothek
{
    public class UpdateImmobilienHypothekByIdCommandHandler
        (ILogger<UpdateImmobilienHypothekByIdCommandHandler> logger,
        IMapper mapper,
        IImmobilienOverviewRepository overviewRepository,
        IBruttomietrenditeRepository bruttomietrenditeRepository,
        IImmobilienHypothekRepository hypothekRepository, IGesamtbelastungRepository gesamtbelastungRepository, 
        IRuecklagenRepository ruecklagenRepository, IImmobilienHausgeldRepository hausgeldRepository) : IRequestHandler<UpdateImmobilienHypothekByIdCommand>
    {
        public async Task Handle(UpdateImmobilienHypothekByIdCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Updating Hypothek with ID: {Id}.", request.Id);

            var hypothek = await hypothekRepository.GetByIdAsync(request.Id);

            if (hypothek == null)
            {
                throw new NotFoundException(nameof(ImmobilienHypothek), request.Id.ToString());
            }

            mapper.Map(request, hypothek);


            var gesamtbelastung = await gesamtbelastungRepository.GetByIdAsync(request.Id);
            if (gesamtbelastung == null)
            {
                throw new NotFoundException(nameof(Gesamtbelastung), request.Id.ToString());
            }
            var ruecklagen = await ruecklagenRepository.GetByIdAsync(request.Id);
            var hausgeld = await hausgeldRepository.GetByIdAsync(request.Id);
            gesamtbelastung.Kreditbelastung = new MonatJahr(hypothek.Kreditbelastung.GesamtKreditbelastung.ProMonat, hypothek.Kreditbelastung.GesamtKreditbelastung.ProJahr);
            gesamtbelastung.Ruecklagen = new MonatJahr(ruecklagen.RuecklagenBetrag.ProMonat, ruecklagen.RuecklagenBetrag.ProJahr);
            gesamtbelastung.NichtUmlagefaehigesHausgeld = new MonatJahr(hausgeld.NichtUmlagefaehigesHausgeld.ProMonat, hausgeld.NichtUmlagefaehigesHausgeld.ProJahr);
            gesamtbelastung.GesamtbelastungBetrag = new MonatJahr((hypothek.Kreditbelastung.GesamtKreditbelastung.ProMonat + ruecklagen.RuecklagenBetrag.ProMonat + hausgeld.NichtUmlagefaehigesHausgeld.ProMonat), (hypothek.Kreditbelastung.GesamtKreditbelastung.ProJahr + ruecklagen.RuecklagenBetrag.ProJahr + hausgeld.NichtUmlagefaehigesHausgeld.ProJahr));


            //TODO: Better update approach for updating kaufpreis
            //if Hypothek kaufpreis has changed
            var overview = await overviewRepository.GetByIdAsync(request.Id);
            overview.Kaufpreis = hypothek.Kaufpreis;

            var bruttomietrendite = await bruttomietrenditeRepository.GetByIdAsync(request.Id);
            bruttomietrendite.Kaufpreis = hypothek.Kaufpreis;

            await bruttomietrenditeRepository.SaveChanges();
            await hypothekRepository.SaveChanges();
            await gesamtbelastungRepository.SaveChanges();
            await overviewRepository.SaveChanges();
        }
    }
}
