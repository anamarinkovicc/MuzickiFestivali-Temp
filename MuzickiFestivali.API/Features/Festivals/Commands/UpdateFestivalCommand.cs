using MediatR;
using MuzickiFestivali.Domain.Interfaces;

namespace MuzickiFestivali.API.Features.Festivals.Commands
{
    public record UpdateFestivalCommand(
       int Id,
       string Naziv,
       string Opis,
       DateTime DatumPocetka,
       DateTime DatumZavrsetka,
       int Kapacitet) : IRequest<bool>;

    public class UpdateFestivalCommandHandler : IRequestHandler<UpdateFestivalCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateFestivalCommandHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public async Task<bool> Handle(UpdateFestivalCommand request, CancellationToken cancellationToken)
        {
            var festival = await _unitOfWork.Festivali.GetByIdAsync(request.Id);

            if (festival == null) return false;

            festival.naziv = request.Naziv;
            festival.opis = request.Opis;
            festival.datumPocetka = request.DatumPocetka;
            festival.datumZavrsetka = request.DatumZavrsetka;
            festival.kapacitet = request.Kapacitet;

            _unitOfWork.Festivali.Update(festival);
            await _unitOfWork.CompleteAsync();

            return true;
        }
    }
}