using AutoMapper;
using BE.Domain.Entities;
using BE.Domain.Entities.Hypothek;
using BE.Domain.Exceptions;
using BE.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BE.Application.Gesamtbelastungen.Commands.UpdateGesamtbelastung
{
    public class UpdateGesamtbelastungCommandHandler
        (ILogger<UpdateGesamtbelastungCommandHandler> logger,
        IMapper mapper,
        IGesamtbelastungRepository gesamtbelastungRepository,
        IImmobilienHypothekRepository hypothekenRepository,
        IRuecklagenRepository ruecklagenRepository,
        IImmobilienHausgeldRepository hausgeldRepository) : IRequestHandler<UpdateGesamtbelastungCommand>
    {
        public async Task Handle(UpdateGesamtbelastungCommand request, CancellationToken cancellationToken)
        {
            var hypothek = await hypothekenRepository.GetByIdAsync(request.Id);
            if (hypothek == null)
            {
                throw new NotFoundException(nameof(ImmobilienHypothek), request.Id.ToString());
            }

            var ruecklagen = await ruecklagenRepository.GetByIdAsync(request.Id);
            if (ruecklagen == null)
            {
                throw new NotFoundException(nameof(Ruecklage), request.Id.ToString());
            }

            var hausgeld = await hausgeldRepository.GetByIdAsync(request.Id);
            if (hausgeld == null)
            {
                throw new NotFoundException(nameof(ImmobilienHausgeld), request.Id.ToString());
            }

            var nichtumlagefaehigesHausgeld = hausgeld.NichtUmlagefaehigesHausgeld;
            var ruecklage = ruecklagen.RuecklagenBetrag;
            var kreditbelastung = hypothek.Kreditbelastung.GesamtKreditbelastung;

            var gesamtbelastungBetrag = new MonatJahr((kreditbelastung.ProMonat + ruecklage.ProMonat + nichtumlagefaehigesHausgeld.ProMonat), (kreditbelastung.ProJahr + ruecklage.ProJahr + nichtumlagefaehigesHausgeld.ProJahr));


            var gesamtbelastung = await gesamtbelastungRepository.GetByIdAsync(request.Id);
            if (gesamtbelastung == null)
            {
                throw new NotFoundException(nameof(Gesamtbelastung), request.Id.ToString());
            }

            gesamtbelastung.Kreditbelastung = new MonatJahr(kreditbelastung.ProMonat, kreditbelastung.ProJahr);
            gesamtbelastung.Ruecklagen = new MonatJahr(ruecklage.ProMonat, ruecklage.ProJahr);
            gesamtbelastung.NichtUmlagefaehigesHausgeld = new MonatJahr(nichtumlagefaehigesHausgeld.ProMonat, nichtumlagefaehigesHausgeld.ProJahr);
            gesamtbelastung.GesamtbelastungBetrag = gesamtbelastungBetrag;

            mapper.Map(request, gesamtbelastung);


            await gesamtbelastungRepository.SaveChanges();

        }
    }
}
