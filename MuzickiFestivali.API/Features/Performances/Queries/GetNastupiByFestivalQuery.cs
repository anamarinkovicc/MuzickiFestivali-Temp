using MediatR;
using MuzickiFestivali.API.DTOs;
using MuzickiFestivali.Domain.Interfaces;

namespace MuzickiFestivali.API.Features.Performances.Queries
{
    public record GetNastupiByFestivalQuery(int IdFestival) : IRequest<List<DisplayNastupDto>>;

    public class GetNastupiByFestivalQueryHandler : IRequestHandler<GetNastupiByFestivalQuery, List<DisplayNastupDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetNastupiByFestivalQueryHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public async Task<List<DisplayNastupDto>> Handle(GetNastupiByFestivalQuery request, CancellationToken cancellationToken)
        {
            var nastupi = await _unitOfWork.Nastupi.GetByFestivalIdAsync(request.IdFestival);

            return nastupi.Select(n => new DisplayNastupDto
            {
                IdNastup = n.idNastup,
                Naziv = n.naziv,
                Opis = n.opis,
                Zanr = n.zanr,
                IdFestival = n.idFestival
            }).ToList();
        }
    }
}
