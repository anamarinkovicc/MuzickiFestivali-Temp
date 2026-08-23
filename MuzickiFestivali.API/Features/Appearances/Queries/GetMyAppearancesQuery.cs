using MediatR;
using MuzickiFestivali.API.DTOs;
using MuzickiFestivali.Domain.Interfaces;

namespace MuzickiFestivali.API.Features.Appearances.Queries
{
    public record GetMyAppearancesQuery(int IdOsoba) : IRequest<List<DisplayMyAppearanceDto>>;

    public class GetMyAppearancesQueryHandler : IRequestHandler<GetMyAppearancesQuery, List<DisplayMyAppearanceDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetMyAppearancesQueryHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public async Task<List<DisplayMyAppearanceDto>> Handle(GetMyAppearancesQuery request, CancellationToken cancellationToken)
        {
            var nastupanja = await _unitOfWork.Nastupanja.GetByPerformerAsync(request.IdOsoba);

            return nastupanja.Select(n => new DisplayMyAppearanceDto
            {
                IdFestival = n.idFestival,
                FestivalNaziv = n.termin.nastup.festival.naziv,

                IdNastup = n.idNastup,
                NastupNaziv = n.termin.nastup.naziv,

                IdTermin = n.idTermin,
                VremePocetka = n.termin.vremePocetka,
                VremeZavrsetka = n.termin.vremeZavrsetka,
                TipTermina = n.termin.tip.ToString(),

                BinaNaziv = n.termin.bina.naziv, 

                Uloga = n.uloga,
                PotvrdjenDolazak = n.potvrdjenDolazak,
                OrganizatorNapomena = n.napomena
            }).ToList();
        }
    }
}
