using MediatR;
using MuzickiFestivali.API.DTOs;
using MuzickiFestivali.Domain.Interfaces;

namespace MuzickiFestivali.API.Features.Slots.Queries
{
    public record GetTerminiByNastupQuery(int IdFestival, int IdNastup) : IRequest<List<DisplayTerminDto>>;

    public class GetTerminiByNastupQueryHandler : IRequestHandler<GetTerminiByNastupQuery, List<DisplayTerminDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetTerminiByNastupQueryHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public async Task<List<DisplayTerminDto>> Handle(GetTerminiByNastupQuery request, CancellationToken cancellationToken)
        {
            var termini = await _unitOfWork.Termini.GetByNastupIdAsync(request.IdFestival, request.IdNastup);

            return termini.Select(t => new DisplayTerminDto
            {
                IdTermin = t.idTermin,
                IdNastup = t.idNastup,
                IdFestival = t.idFestival,
                VremePocetka = t.vremePocetka,
                VremeZavrsetka = t.vremeZavrsetka,
                Tip = t.tip.ToString(),
                IdBina = t.idBina,
                Napomena = t.napomena
            }).ToList();
        }
    }
}
