using MediatR;
using MuzickiFestivali.Domain.Interfaces;

namespace MuzickiFestivali.API.Features.Appearances.Commands
{
    public record RemoveIzvodjacFromTerminCommand(int IdOsoba, int IdFestival, int IdNastup, int IdTermin) : IRequest<bool>;

    public class RemovePerformerFromSlotCommandHandler : IRequestHandler<RemoveIzvodjacFromTerminCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public RemovePerformerFromSlotCommandHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public async Task<bool> Handle(RemoveIzvodjacFromTerminCommand request, CancellationToken cancellationToken)
        {
            var nastupanje = await _unitOfWork.Nastupanja.GetByKeyAsync(request.IdOsoba, request.IdFestival, request.IdNastup, request.IdTermin);
            if (nastupanje == null) return false;

            _unitOfWork.Nastupanja.Delete(nastupanje);
            await _unitOfWork.CompleteAsync();
            return true;
        }
    }
}
