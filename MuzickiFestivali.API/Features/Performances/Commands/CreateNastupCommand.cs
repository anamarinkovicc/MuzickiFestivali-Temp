using MediatR;
using MuzickiFestivali.Domain.Entities;
using MuzickiFestivali.Domain.Enums;
using MuzickiFestivali.Domain.Interfaces;

namespace MuzickiFestivali.API.Features.Performances.Commands
{
    public record CreateNastupCommand(string Naziv, string Opis, Zanr Zanr, int IdFestival) : IRequest<int>;

    public class CreateNastupCommandHandler : IRequestHandler<CreateNastupCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateNastupCommandHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public async Task<int> Handle(CreateNastupCommand request, CancellationToken cancellationToken)
        {
            var noviNastup = new Nastup
            {
                naziv = request.Naziv,
                opis = request.Opis,
                zanr = request.Zanr,
                idFestival = request.IdFestival
            };

            await _unitOfWork.Nastupi.AddAsync(noviNastup);
            await _unitOfWork.CompleteAsync();

            return noviNastup.idNastup;
        }
    }
}
