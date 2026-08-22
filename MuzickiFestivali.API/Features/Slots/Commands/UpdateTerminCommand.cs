using MediatR;
using MuzickiFestivali.Domain.Enums;
using MuzickiFestivali.Domain.Interfaces;

namespace MuzickiFestivali.API.Features.Slots.Commands
{
    public record UpdateTerminCommand(
        int IdFestival,
        int IdNastup,
        int IdTermin,
        DateTime VremePocetka,
        DateTime VremeZavrsetka,
        TipTermina Tip,
        int IdBina,
        string? Napomena) : IRequest<bool>;

    public class UpdateTerminCommandHandler : IRequestHandler<UpdateTerminCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateTerminCommandHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public async Task<bool> Handle(UpdateTerminCommand request, CancellationToken cancellationToken)
        {
            var termin = await _unitOfWork.Termini.GetByIdAsync(request.IdFestival, request.IdNastup, request.IdTermin);

            if (termin == null) return false;

            termin.vremePocetka = request.VremePocetka;
            termin.vremeZavrsetka = request.VremeZavrsetka;
            termin.tip = request.Tip;
            termin.idBina = request.IdBina;
            termin.napomena = request.Napomena;

            _unitOfWork.Termini.Update(termin);
            await _unitOfWork.CompleteAsync();

            return true;
        }
    }
}
