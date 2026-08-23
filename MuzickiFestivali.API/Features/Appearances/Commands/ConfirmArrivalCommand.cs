using MediatR;
using MuzickiFestivali.Domain.Interfaces;

namespace MuzickiFestivali.API.Features.Appearances.Commands
{
    public record ConfirmArrivalCommand(
         int IdOsoba,
         int IdFestival,
         int IdNastup,
         int IdTermin) : IRequest<bool>;

    public class ConfirmArrivalCommandHandler : IRequestHandler<ConfirmArrivalCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ConfirmArrivalCommandHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public async Task<bool> Handle(ConfirmArrivalCommand request, CancellationToken cancellationToken)
        {
            var nastupanje = await _unitOfWork.Nastupanja.GetByKeyAsync(
                request.IdOsoba,
                request.IdFestival,
                request.IdNastup,
                request.IdTermin
            );

            if (nastupanje == null) return false;

            nastupanje.potvrdjenDolazak = true; 

            _unitOfWork.Nastupanja.Update(nastupanje);
            await _unitOfWork.CompleteAsync();

            return true;
        }
    }
}
