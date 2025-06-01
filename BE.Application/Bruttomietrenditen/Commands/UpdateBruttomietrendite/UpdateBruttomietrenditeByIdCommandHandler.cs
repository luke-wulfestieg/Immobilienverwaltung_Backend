using AutoMapper;
using BE.Domain.Entities;
using BE.Domain.Exceptions;
using BE.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BE.Application.Bruttomietrenditen.Commands.UpdateBruttomietrendite
{
    public class UpdateBruttomietrenditeByIdCommandHandler
         (ILogger<UpdateBruttomietrenditeByIdCommandHandler> logger,
        IMapper mapper,
        IBruttomietrenditeRepository bruttomietrenditeRepository,
        IRuecklagenRepository ruecklagenRepository) : IRequestHandler<UpdateBruttomietrenditeByIdCommand>
    {
        public async Task Handle(UpdateBruttomietrenditeByIdCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Updating Hausgeld with ID: {Id}. New Values: Kaltmiete: {Kaltmiete}, Warmmiete: {Warmmiete}, BruttoMietrendite: {Bruttomietrendite} ...", request.Id, request.Kaltmiete, request.Warmmiete, request.BruttomietrenditeBetrag);

            var bruttomietrendite = await bruttomietrenditeRepository.GetByIdAsync(request.Id);

            if (bruttomietrendite == null)
            {
                throw new NotFoundException(nameof(Bruttomietrendite), request.Id.ToString());
            }
            mapper.Map(request, bruttomietrendite);

            var ruecklagen = await ruecklagenRepository.GetByIdAsync(request.Id);
            var mietausfallProzent = ruecklagen.Mietausfall.InProzent;
           
            ruecklagen.Mietausfall = new ProzentMonatJahr(mietausfallProzent,
                (mietausfallProzent / 100) * bruttomietrendite.Kaltmiete.ProMonat,
                (mietausfallProzent / 100) * bruttomietrendite.Kaltmiete.ProJahr);

            var ruecklagenBetragPM = ruecklagen.Instandhaltung.ProMonat + ruecklagen.Mietausfall.ProMonat;
            var ruecklagenBetragPA = ruecklagen.Instandhaltung.ProJahr + ruecklagen.Mietausfall.ProJahr;

            ruecklagen.RuecklagenBetrag = new MonatJahr(ruecklagenBetragPM, ruecklagenBetragPA);

            await ruecklagenRepository.SaveChanges();
            await bruttomietrenditeRepository.SaveChanges();
        }
    }
}
