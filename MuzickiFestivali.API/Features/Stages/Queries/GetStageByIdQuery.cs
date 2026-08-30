using MediatR;
using MuzickiFestivali.API.DTOs;
using MuzickiFestivali.Domain.Interfaces;

namespace MuzickiFestivali.API.Features.Stages.Queries
{
    public record GetStageByIdQuery(int Id) : IRequest<DisplayBinaDto?>;

    public class GetStageByIdQueryHandler : IRequestHandler<GetStageByIdQuery, DisplayBinaDto?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetStageByIdQueryHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public async Task<DisplayBinaDto?> Handle(GetStageByIdQuery request, CancellationToken cancellationToken)
        {
            var b = await _unitOfWork.Bine.GetByIdAsync(request.Id);

            if (b == null) return null;

            return new DisplayBinaDto
            {
                IdBina = b.idBina,
                Naziv = b.naziv,
                Kapacitet = b.kapacitet,
                XKoordinata = b.xKoordinata,
                YKoordinata = b.yKoordinata
            };
        }
    }
}
