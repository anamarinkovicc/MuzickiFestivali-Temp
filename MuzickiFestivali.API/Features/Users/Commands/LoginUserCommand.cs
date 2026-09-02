using MediatR;
using MuzickiFestivali.API.DTOs;
using MuzickiFestivali.Domain.Entities;
using MuzickiFestivali.Domain.Interfaces;

namespace MuzickiFestivali.API.Features.Auth.Commands
{
    public record LoginUserCommand(string Email, string Lozinka) : IRequest<LoggedUserDto?>;

    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoggedUserDto?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public LoginUserCommandHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public async Task<LoggedUserDto?> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var osoba = await _unitOfWork.Osobe.GetByEmailAndPasswordAsync(request.Email, request.Lozinka);

            if (osoba == null) return null;

            string uloga = osoba switch
            {
                Zaposleni => "Zaposleni",
                Izvodjac => "Izvodjac", 
                Korisnik => "Korisnik",
                _ => "Korisnik" 
            };

            return new LoggedUserDto
            {
                IdOsoba = osoba.idOsoba,
                Email = osoba.email,
                Ime = osoba.ime,
                Prezime = osoba.prezime,
                Uloga = uloga
            };
        }
    }
}
