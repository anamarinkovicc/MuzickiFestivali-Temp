using MediatR;
using MuzickiFestivali.Domain.Interfaces;

namespace MuzickiFestivali.API.Features.Appearances.Commands
{
    public record UpdateNastupaCommand(
        int IdOsoba,
        int IdFestival,
        int IdNastup,
        int IdTermin,
        string Uloga,
        bool PotvrdjenDolazak,
        string? Napomena) : IRequest<bool>;

    public class UpdateNastupaCommandHandler : IRequestHandler<UpdateNastupaCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateNastupaCommandHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public async Task<bool> Handle(UpdateNastupaCommand request, CancellationToken cancellationToken)
        {
            var nastupanje = await _unitOfWork.Nastupanja.GetByKeyAsync(request.IdOsoba, request.IdFestival, request.IdNastup, request.IdTermin);
            if (nastupanje == null) return false;

            nastupanje.uloga = request.Uloga;
            nastupanje.potvrdjenDolazak = request.PotvrdjenDolazak;
            nastupanje.napomena = request.Napomena;

            _unitOfWork.Nastupanja.Update(nastupanje);
            await _unitOfWork.CompleteAsync();
            return true;
        }
    }
}
