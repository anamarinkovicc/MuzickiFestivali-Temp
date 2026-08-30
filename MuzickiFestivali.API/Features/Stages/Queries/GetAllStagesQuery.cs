using MediatR;
using MuzickiFestivali.API.DTOs;
using MuzickiFestivali.Domain.Interfaces;

namespace MuzickiFestivali.API.Features.Stages.Queries
{
    public record GetAllStagesQuery() : IRequest<List<DisplayBinaDto>>;

    public class GetAllStagesQueryHandler : IRequestHandler<GetAllStagesQuery, List<DisplayBinaDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllStagesQueryHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public async Task<List<DisplayBinaDto>> Handle(GetAllStagesQuery request, CancellationToken cancellationToken)
        {
            var bine = await _unitOfWork.Bine.GetAllAsync();

            return bine.Select(b => new DisplayBinaDto
            {
                IdBina = b.idBina,
                Naziv = b.naziv,
                Kapacitet = b.kapacitet,
                XKoordinata = b.xKoordinata,
                YKoordinata = b.yKoordinata
            }).ToList();
        }
    }
}
