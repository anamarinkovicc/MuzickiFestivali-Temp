using MediatR;
using MuzickiFestivali.API.DTOs;
using MuzickiFestivali.Domain.Interfaces;

namespace MuzickiFestivali.API.Features.Likes.Queries
{
    public record GetMyLikedNastupiQuery(int IdOsoba) : IRequest<List<DisplayLikedPerformanceDto>>;

    public class GetMyLikedPerformancesQueryHandler : IRequestHandler<GetMyLikedNastupiQuery, List<DisplayLikedPerformanceDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetMyLikedPerformancesQueryHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public async Task<List<DisplayLikedPerformanceDto>> Handle(GetMyLikedNastupiQuery request, CancellationToken cancellationToken)
        {
            var lajkovi = await _unitOfWork.Lajkovi.GetLikesByKorisnikAsync(request.IdOsoba);

            return lajkovi.Select(l => new DisplayLikedPerformanceDto
            {
                IdNastup = l.idNastup,
                IdFestival = l.idFestival,
                NazivNastupa = l.nastup.naziv,
                OpisNastupa = l.nastup.opis,
                Zanr = l.nastup.zanr.ToString(),
                DatumVremeLajka = l.datumVremeLajka
            }).ToList();
        }
    }
}
