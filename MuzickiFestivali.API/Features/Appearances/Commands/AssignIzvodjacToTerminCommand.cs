using MediatR;
using MuzickiFestivali.Domain.Interfaces;
using MuzickiFestivali.Domain.Entities;

namespace MuzickiFestivali.API.Features.Appearances.Commands
{
    public record AssignIzvodjacToTerminCommand(
        int IdOsoba,
        int IdFestival,
        int IdNastup,
        int IdTermin,
        string Uloga,
        string? Napomena) : IRequest<bool>;

    public class AssignIzvodjacToTerminCommandHandler : IRequestHandler<AssignIzvodjacToTerminCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AssignIzvodjacToTerminCommandHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public async Task<bool> Handle(AssignIzvodjacToTerminCommand request, CancellationToken cancellationToken)
        {
            var izvodjac = await _unitOfWork.Izvodjaci.GetByIdAsync(request.IdOsoba);
            if (izvodjac == null) return false;

            var termin = await _unitOfWork.Termini.GetByIdAsync(request.IdFestival, request.IdNastup, request.IdTermin);
            if (termin == null) return false;

            var postoji = await _unitOfWork.Nastupanja.GetByKeyAsync(request.IdOsoba, request.IdFestival, request.IdNastup, request.IdTermin);
            if (postoji != null) return true;

            var nastupanje = new Nastupa
            {
                idOsoba = request.IdOsoba,
                idFestival = request.IdFestival,
                idNastup = request.IdNastup,
                idTermin = request.IdTermin,
                uloga = request.Uloga,
                potvrdjenDolazak = false, 
                napomena = request.Napomena
            };

            await _unitOfWork.Nastupanja.AddAsync(nastupanje);
            await _unitOfWork.CompleteAsync();
            return true;
        }
    }
}
