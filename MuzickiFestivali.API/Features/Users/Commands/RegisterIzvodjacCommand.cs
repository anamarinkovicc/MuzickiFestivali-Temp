using MediatR;
using MuzickiFestivali.Domain.Entities;
using MuzickiFestivali.Domain.Enums;
using MuzickiFestivali.Domain.Interfaces;

namespace MuzickiFestivali.API.Features.Users.Commands
{
    public record RegisterIzvodjacCommand(
        string Ime,
        string Prezime,
        string Email,
        string Lozinka,
        string UmetnickoIme,
        string Biografija,
        Zanr Zanr) : IRequest<int>;

    public class RegisterIzvodjacCommandHandler : IRequestHandler<RegisterIzvodjacCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;

        public RegisterIzvodjacCommandHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public async Task<int> Handle(RegisterIzvodjacCommand request, CancellationToken cancellationToken)
        {
            var noviIzvodjac = new Izvodjac
            {
                ime = request.Ime,
                prezime = request.Prezime,
                email = request.Email,
                lozinka = request.Lozinka,
                umetnickoIme = request.UmetnickoIme,
                biografija = request.Biografija,
                zanr = request.Zanr
            };

            await _unitOfWork.Izvodjaci.AddAsync(noviIzvodjac);
            await _unitOfWork.CompleteAsync();

            return noviIzvodjac.idOsoba;
        }
    }
}
