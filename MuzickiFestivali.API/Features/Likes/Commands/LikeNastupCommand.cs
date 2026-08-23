using MediatR;
using MuzickiFestivali.Domain.Entities;
using MuzickiFestivali.Domain.Interfaces;

namespace MuzickiFestivali.API.Features.Likes.Commands
{
    public record LikeNastupCommand(int IdFestival, int IdNastup, int IdOsoba) : IRequest<bool>;

    public class LikePerformanceCommandHandler : IRequestHandler<LikeNastupCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public LikePerformanceCommandHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public async Task<bool> Handle(LikeNastupCommand request, CancellationToken cancellationToken)
        {
            var nastup = await _unitOfWork.Nastupi.GetByIdAsync(request.IdFestival, request.IdNastup);
            if (nastup == null) return false;

            var postojeciLajk = await _unitOfWork.Lajkovi.GetByKeyAsync(request.IdOsoba, request.IdFestival, request.IdNastup);
            if (postojeciLajk != null) return true; 

            var noviLajk = new Lajkuje
            {
                idOsoba = request.IdOsoba,
                idFestival = request.IdFestival,
                idNastup = request.IdNastup,
                datumVremeLajka = DateTime.Now
            };

            await _unitOfWork.Lajkovi.AddAsync(noviLajk);
            await _unitOfWork.CompleteAsync();

            return true;
        }
    }
}
