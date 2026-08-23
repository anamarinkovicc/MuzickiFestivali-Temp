using MediatR;
using MuzickiFestivali.Domain.Interfaces;

namespace MuzickiFestivali.API.Features.Likes.Commands
{
    public record UnlikeNastupCommand(int IdFestival, int IdNastup, int IdOsoba) : IRequest<bool>;

    public class UnlikePerformanceCommandHandler : IRequestHandler<UnlikeNastupCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UnlikePerformanceCommandHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public async Task<bool> Handle(UnlikeNastupCommand request, CancellationToken cancellationToken)
        {
            var lajk = await _unitOfWork.Lajkovi.GetByKeyAsync(request.IdOsoba, request.IdFestival, request.IdNastup);
            if (lajk == null) return false; 

            _unitOfWork.Lajkovi.Delete(lajk);
            await _unitOfWork.CompleteAsync();

            return true;
        }
    }
}
