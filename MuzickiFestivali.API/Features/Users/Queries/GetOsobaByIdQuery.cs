using MediatR;
using MuzickiFestivali.API.DTOs;
using MuzickiFestivali.Domain.Entities;
using MuzickiFestivali.Domain.Interfaces;

namespace MuzickiFestivali.API.Features.Users.Queries
{
    public record GetOsobaByIdQuery(int Id) : IRequest<DisplayUserDto?>;

    public class GetOsobaByIdQueryHandler : IRequestHandler<GetOsobaByIdQuery, DisplayUserDto?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetOsobaByIdQueryHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public async Task<DisplayUserDto?> Handle(GetOsobaByIdQuery request, CancellationToken cancellationToken)
        {
            var osoba = await _unitOfWork.Osobe.GetByIdAsync(request.Id);

            if (osoba == null) return null;

            string uloga = "Korisnik";
            string? pozicija = null;
            string? umetnickoIme = null;

            if (osoba is Zaposleni z)
            {
                uloga = "Zaposleni";
                pozicija = z.pozicija;
            }
            else if (osoba is Izvodjac izv)
            {
                uloga = "Izvodjac";
                umetnickoIme = izv.umetnickoIme;
            }
            else if (osoba is Korisnik)
            {
                uloga = "Korisnik";
            }

            return new DisplayUserDto
            {
                UserId = osoba.idOsoba,
                Ime = osoba.ime,
                Prezime = osoba.prezime,
                Email = osoba.email,
                Uloga = uloga,
                Pozicija = pozicija,
                UmetnickoIme = umetnickoIme
            };
        }
    }
}
