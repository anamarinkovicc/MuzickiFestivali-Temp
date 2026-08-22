using MediatR;
using MuzickiFestivali.Domain.Entities;
using MuzickiFestivali.Domain.Enums;
using MuzickiFestivali.Domain.Interfaces;

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

        public CreateTerminCommandHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public async Task<int> Handle(CreateTerminCommand request, CancellationToken cancellationToken)
        {
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
