using MediatR;
using MuzickiFestivali.Domain.Enums;
using MuzickiFestivali.Domain.Interfaces;

namespace MuzickiFestivali.API.Features.Performances.Commands
{
    public record UpdateNastupCommand(int IdFestival, int IdNastup, string Naziv, string Opis, Zanr Zanr) : IRequest<bool>;

    public class UpdateNastupCommandHandler : IRequestHandler<UpdateNastupCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateNastupCommandHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public async Task<bool> Handle(UpdateNastupCommand request, CancellationToken cancellationToken)
        {
            var nastup = await _unitOfWork.Nastupi.GetByIdAsync(request.IdFestival, request.IdNastup);

            if (nastup == null) return false;

            nastup.naziv = request.Naziv;
            nastup.opis = request.Opis;
            nastup.zanr = request.Zanr;

            _unitOfWork.Nastupi.Update(nastup);
            await _unitOfWork.CompleteAsync();

            return true;
        }
    }
}
