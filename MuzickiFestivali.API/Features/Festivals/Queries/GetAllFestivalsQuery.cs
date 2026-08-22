using MediatR;
using MuzickiFestivali.API.DTOs;
using MuzickiFestivali.Domain.Interfaces;

namespace MuzickiFestivali.API.Features.Festivals.Queries
{
    public record GetAllFestivalsQuery() : IRequest<List<DisplayFestivalDto>>;

    public class GetAllFestivalsQueryHandler : IRequestHandler<GetAllFestivalsQuery, List<DisplayFestivalDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllFestivalsQueryHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public async Task<List<DisplayFestivalDto>> Handle(GetAllFestivalsQuery request, CancellationToken cancellationToken)
        {
            var festivali = await _unitOfWork.Festivali.GetAllAsync();

            return festivali.Select(f => new DisplayFestivalDto
            {
                IdFestival = f.idFestival,
                Naziv = f.naziv,
                Opis = f.opis,
                DatumPocetka = f.datumPocetka,
                DatumZavrsetka = f.datumZavrsetka,
                Kapacitet = f.kapacitet
            }).ToList();
        }
    }
}
