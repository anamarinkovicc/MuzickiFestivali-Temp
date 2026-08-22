using MediatR;
using MuzickiFestivali.Domain.Interfaces;

namespace MuzickiFestivali.API.Features.Festivals.Commands
{
    public record DeleteFestivalCommand(int Id) : IRequest<bool>;

    public class DeleteFestivalCommandHandler : IRequestHandler<DeleteFestivalCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteFestivalCommandHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public async Task<bool> Handle(DeleteFestivalCommand request, CancellationToken cancellationToken)
        {
            var festival = await _unitOfWork.Festivali.GetByIdAsync(request.Id);

            if (festival == null) return false;

            _unitOfWork.Festivali.Delete(festival);
            await _unitOfWork.CompleteAsync();

            return true;
        }
    }
}
