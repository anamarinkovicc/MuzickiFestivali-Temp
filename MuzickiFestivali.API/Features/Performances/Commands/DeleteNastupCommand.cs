using MediatR;
using MuzickiFestivali.Domain.Interfaces;

namespace MuzickiFestivali.API.Features.Performances.Commands
{
    public record DeleteNastupCommand(int IdFestival, int IdNastup) : IRequest<bool>;

    public class DeleteNastupCommandHandler : IRequestHandler<DeleteNastupCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteNastupCommandHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public async Task<bool> Handle(DeleteNastupCommand request, CancellationToken cancellationToken)
        {
            var nastup = await _unitOfWork.Nastupi.GetByIdAsync(request.IdFestival, request.IdNastup);

            if (nastup == null) return false;

            _unitOfWork.Nastupi.Delete(nastup);
            await _unitOfWork.CompleteAsync();

            return true;
        }
    }
}
