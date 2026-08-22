using MediatR;
using MuzickiFestivali.API.DTOs;
using MuzickiFestivali.Domain.Interfaces;

namespace MuzickiFestivali.API.Features.Festivals.Queries
{
    public record GetFestivalByIdQuery(int Id) : IRequest<DisplayFestivalDto?>;

    public class GetFestivalByIdQueryHandler : IRequestHandler<GetFestivalByIdQuery, DisplayFestivalDto?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetFestivalByIdQueryHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public async Task<DisplayFestivalDto?> Handle(GetFestivalByIdQuery request, CancellationToken cancellationToken)
        {
            var f = await _unitOfWork.Festivali.GetByIdAsync(request.Id);

            if (f == null) return null;

            return new DisplayFestivalDto
            {
                IdFestival = f.idFestival,
                Naziv = f.naziv,
                Opis = f.opis,
                DatumPocetka = f.datumPocetka,
                DatumZavrsetka = f.datumZavrsetka,
                Kapacitet = f.kapacitet
            };
        }
    }
}
