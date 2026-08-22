using MediatR;
using MuzickiFestivali.Domain.Entities;
using MuzickiFestivali.Domain.Enums;
using MuzickiFestivali.Domain.Interfaces;

namespace MuzickiFestivali.API.Features.Users.Commands
{
    public record RegisterKorisnikCommand(
        string Ime,
        string Prezime,
        string Email,
        string Lozinka,
        Zanr OmiljeniZanr) : IRequest<int>;

    public class RegisterKorisnikCommandHandler : IRequestHandler<RegisterKorisnikCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;

        public RegisterKorisnikCommandHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public async Task<int> Handle(RegisterKorisnikCommand request, CancellationToken cancellationToken)
        {
            var noviKorisnik = new Korisnik
            {
                ime = request.Ime,
                prezime = request.Prezime,
                email = request.Email,
                lozinka = request.Lozinka,
                omiljeniZanr = request.OmiljeniZanr
            };

            await _unitOfWork.Korisnici.AddAsync(noviKorisnik);
            await _unitOfWork.CompleteAsync();

            return noviKorisnik.idOsoba;
        }
    }
}
