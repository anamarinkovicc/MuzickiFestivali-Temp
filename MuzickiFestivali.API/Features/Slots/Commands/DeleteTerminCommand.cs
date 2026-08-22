using MediatR;
using MuzickiFestivali.Domain.Interfaces;

namespace MuzickiFestivali.API.Features.Slots.Commands
{
    public record DeleteTerminCommand(int IdFestival, int IdNastup, int IdTermin) : IRequest<bool>;

    public class DeleteTerminCommandHandler : IRequestHandler<DeleteTerminCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteTerminCommandHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public async Task<bool> Handle(DeleteTerminCommand request, CancellationToken cancellationToken)
        {
            var termin = await _unitOfWork.Termini.GetByIdAsync(request.IdFestival, request.IdNastup, request.IdTermin);

            if (termin == null) return false;

            _unitOfWork.Termini.Delete(termin); 
            await _unitOfWork.CompleteAsync();

            return true;
        }
    }
}
