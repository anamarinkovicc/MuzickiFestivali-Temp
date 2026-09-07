using MediatR;
using Microsoft.Extensions.Localization;
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
        int IdOsoba,
        string? SlikaUrl) : IRequest<int>;

    public class CreateFestivalCommandHandler : IRequestHandler<CreateFestivalCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStringLocalizer<SharedResources> _localizer;

        public CreateFestivalCommandHandler(IUnitOfWork unitOfWork, IStringLocalizer<SharedResources> localizer)
        {
            _unitOfWork = unitOfWork;
            _localizer = localizer;
        }

        public async Task<int> Handle(CreateFestivalCommand request, CancellationToken cancellationToken)
        {
            if (request.DatumZavrsetka < request.DatumPocetka)
            {
                throw new ArgumentException(_localizer["Festival_InvalidDateRange"].Value);
            }
            var festival = new Festival
            {
                naziv = request.Naziv,
                opis = request.Opis,
                datumPocetka = request.DatumPocetka,
                datumZavrsetka = request.DatumZavrsetka,
                kapacitet = request.Kapacitet,
                idOsoba = request.IdOsoba,
                SlikaUrl = request.SlikaUrl
            };

            await _unitOfWork.Festivali.AddAsync(festival);
            await _unitOfWork.CompleteAsync();

            return festival.idFestival;
        }
    }
}
