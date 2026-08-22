using MediatR;
using MuzickiFestivali.API.DTOs;
using MuzickiFestivali.Domain.Interfaces;

namespace MuzickiFestivali.API.Features.Performances.Queries
{
    public record GetNastupByIdQuery(int IdFestival, int IdNastup) : IRequest<DisplayNastupDto?>;

    public class GetNastupByIdQueryHandler : IRequestHandler<GetNastupByIdQuery, DisplayNastupDto?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetNastupByIdQueryHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public async Task<DisplayNastupDto?> Handle(GetNastupByIdQuery request, CancellationToken cancellationToken)
        {
            var n = await _unitOfWork.Nastupi.GetByIdAsync(request.IdFestival, request.IdNastup);

            if (n == null) return null;

            return new DisplayNastupDto
            {
                IdNastup = n.idNastup,
                IdFestival = n.idFestival,
                Naziv = n.naziv,
                Opis = n.opis,
                Zanr = n.zanr
            };
        }
    }
}
