using MediatR;
using MuzickiFestivali.API.DTOs;
using MuzickiFestivali.Domain.Interfaces;

namespace MuzickiFestivali.API.Features.Users.Queries
{
    public record GetAllIzvodjaciQuery() : IRequest<List<DisplayIzvodjacDto>>;

    public class GetAllIzvodjaciQueryHandler : IRequestHandler<GetAllIzvodjaciQuery, List<DisplayIzvodjacDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllIzvodjaciQueryHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public async Task<List<DisplayIzvodjacDto>> Handle(GetAllIzvodjaciQuery request, CancellationToken cancellationToken)
        {
            var izvodjaci = await _unitOfWork.Izvodjaci.GetAllAsync();

            return izvodjaci.Select(i => new DisplayIzvodjacDto
            {
                IdOsoba = i.idOsoba,
                Ime = i.ime,
                Prezime = i.prezime,
                Email = i.email,
                UmetnickoIme = i.umetnickoIme,
                Biografija = i.biografija,
                Zanr = i.zanr.ToString() 
            }).ToList();
        }
    }
}
