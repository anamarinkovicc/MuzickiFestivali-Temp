using MediatR;
using MuzickiFestivali.API.DTOs;
using MuzickiFestivali.Domain.Interfaces;

namespace MuzickiFestivali.API.Features.Appearances.Queries
{
    public record GetIzvodjaciByTerminQuery(int IdFestival, int IdNastup, int IdTermin) : IRequest<List<DisplayNastupaDto>>;

    public class GetPerformersBySlotQueryHandler : IRequestHandler<GetIzvodjaciByTerminQuery, List<DisplayNastupaDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetPerformersBySlotQueryHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public async Task<List<DisplayNastupaDto>> Handle(GetIzvodjaciByTerminQuery request, CancellationToken cancellationToken)
        {
            var nastupanja = await _unitOfWork.Nastupanja.GetBySlotAsync(request.IdFestival, request.IdNastup, request.IdTermin);

            return nastupanja.Select(n => new DisplayNastupaDto
            {
                IdOsoba = n.idOsoba,
                Ime = n.izvodjac.ime,
                Prezime = n.izvodjac.prezime,
                UmetnickoIme = n.izvodjac.umetnickoIme,
                Uloga = n.uloga,
                PotvrdjenDolazak = n.potvrdjenDolazak,
                Napomena = n.napomena
            }).ToList();
        }
    }
}
