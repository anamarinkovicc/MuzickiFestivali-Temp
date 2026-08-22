using MediatR;
using MuzickiFestivali.Domain.Entities;
using MuzickiFestivali.Domain.Interfaces;

namespace MuzickiFestivali.API.Features.Users.Commands
{
    public record RegisterZaposleniCommand(
       string Ime,
       string Prezime,
       string Email,
       string Lozinka,
       string Pozicija) : IRequest<int>;

    public class RegisterZaposleniCommandHandler : IRequestHandler<RegisterZaposleniCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;

        public RegisterZaposleniCommandHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public async Task<int> Handle(RegisterZaposleniCommand request, CancellationToken cancellationToken)
        {
            var noviZaposleni = new Zaposleni
            {
                ime = request.Ime,
                prezime = request.Prezime,
                email = request.Email,
                lozinka = request.Lozinka,
                pozicija = request.Pozicija
            };

            await _unitOfWork.Zaposleni.AddAsync(noviZaposleni);
            await _unitOfWork.CompleteAsync();

            return noviZaposleni.idOsoba;
        }
    }
}
