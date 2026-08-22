using MediatR;
using MuzickiFestivali.Domain.Entities;
using MuzickiFestivali.Domain.Interfaces;

namespace MuzickiFestivali.API.Features.Festivals.Commands
{
    public record CreateFestivalCommand(
        string Naziv,
        string Opis,
        DateTime DatumPocetka,
        DateTime DatumZavrsetka,
        int Kapacitet,
        int IdOsoba) : IRequest<int>;

    public class CreateFestivalCommandHandler : IRequestHandler<CreateFestivalCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateFestivalCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(CreateFestivalCommand request, CancellationToken cancellationToken)
        {
            var festival = new Festival
            {
                naziv = request.Naziv,
                opis = request.Opis,
                datumPocetka = request.DatumPocetka,
                datumZavrsetka = request.DatumZavrsetka,
                kapacitet = request.Kapacitet,
                idOsoba = request.IdOsoba
            };

            await _unitOfWork.Festivali.AddAsync(festival);
            await _unitOfWork.CompleteAsync();

            return festival.idFestival;
        }
    }
}
