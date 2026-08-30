using MediatR;
using MuzickiFestivali.Domain.Entities;
using MuzickiFestivali.Domain.Enums;
using MuzickiFestivali.Domain.Interfaces;
using Microsoft.Extensions.Localization;

namespace MuzickiFestivali.API.Features.Slots.Commands
{
    public record CreateTerminCommand(
        int IdFestival,
        int IdNastup,
        DateTime VremePocetka,
        DateTime VremeZavrsetka,
        TipTermina Tip,
        int IdBina,
        string? Napomena) : IRequest<int>;

    public class CreateTerminCommandHandler : IRequestHandler<CreateTerminCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStringLocalizer<SharedResources> _localizer;

        public CreateTerminCommandHandler(IUnitOfWork unitOfWork, IStringLocalizer<SharedResources> localizer)
        {
            _unitOfWork = unitOfWork;
            _localizer = localizer;
        }
        public async Task<int> Handle(CreateTerminCommand request, CancellationToken cancellationToken)
        {
            var festival = await _unitOfWork.Festivali.GetByIdAsync(request.IdFestival);
            if (festival == null)
            {
                throw new KeyNotFoundException(_localizer["Festival_NotFound"].Value);
            }

            if (request.VremePocetka >= request.VremeZavrsetka)
            {
                throw new ArgumentException(_localizer["Slot_InvalidTimeRange"].Value);
            }

            if (request.VremePocetka < festival.datumPocetka || request.VremeZavrsetka > festival.datumZavrsetka)
            {
                throw new ArgumentException(_localizer["Slot_OutsideFestivalDates"].Value);
            }
            var noviTermin = new Termin
            {
                idFestival = request.IdFestival,
                idNastup = request.IdNastup,
                vremePocetka = request.VremePocetka,
                vremeZavrsetka = request.VremeZavrsetka,
                tip = request.Tip,
                idBina = request.IdBina,
                napomena = request.Napomena
            };

            await _unitOfWork.Termini.AddAsync(noviTermin);
            await _unitOfWork.CompleteAsync();

            return noviTermin.idTermin;
        }
    }
}
