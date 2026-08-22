using MediatR;
using MuzickiFestivali.API.DTOs;
using MuzickiFestivali.Domain.Interfaces;

namespace MuzickiFestivali.API.Features.Slots.Queries
{
    public record GetTerminByIdQuery(int IdFestival, int IdNastup, int IdTermin) : IRequest<DisplayTerminDto?>;

    public class GetTerminByIdQueryHandler : IRequestHandler<GetTerminByIdQuery, DisplayTerminDto?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetTerminByIdQueryHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public async Task<DisplayTerminDto?> Handle(GetTerminByIdQuery request, CancellationToken cancellationToken)
        {
            var t = await _unitOfWork.Termini.GetByIdAsync(request.IdFestival, request.IdNastup, request.IdTermin);

            if (t == null) return null;

            return new DisplayTerminDto
            {
                IdTermin = t.idTermin,
                IdNastup = t.idNastup,
                IdFestival = t.idFestival,
                VremePocetka = t.vremePocetka,
                VremeZavrsetka = t.vremeZavrsetka,
                Tip = t.tip.ToString(),
                IdBina = t.idBina,
                Napomena = t.napomena
            };
        }
    }
}
