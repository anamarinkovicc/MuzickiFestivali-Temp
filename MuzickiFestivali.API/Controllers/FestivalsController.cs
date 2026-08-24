using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using MuzickiFestivali.API.DTOs;
using MuzickiFestivali.API.Features.Festivals.Commands;
using MuzickiFestivali.API.Features.Festivals.Queries;

namespace MuzickiFestivali.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FestivalsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IStringLocalizer<SharedResources> _localizer;

        public FestivalsController(IMediator mediator, IStringLocalizer<SharedResources> localizer)
        {
            _mediator = mediator;
            _localizer = localizer;
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create(FestivalDto dto)
        {
            int? trenutniKorisnikId = HttpContext.Session.GetInt32("UserId");

            if (trenutniKorisnikId == null)
                return Unauthorized(_localizer["User_Unauthorized"].Value);

            var command = new CreateFestivalCommand(
                dto.Naziv,
                dto.Opis,
                dto.DatumPocetka,
                dto.DatumZavrsetka,
                dto.Kapacitet,
                trenutniKorisnikId.Value
            );

            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<List<DisplayFestivalDto>>> GetAll()
        {
            var result = await _mediator.Send(new GetAllFestivalsQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DisplayFestivalDto>> GetById(int id)
        {
            var result = await _mediator.Send(new GetFestivalByIdQuery(id));

            if (result == null)
                return NotFound(_localizer["Festival_NotFound"].Value);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, FestivalDto dto)
        {
            var command = new UpdateFestivalCommand(
                id,
                dto.Naziv,
                dto.Opis,
                dto.DatumPocetka,
                dto.DatumZavrsetka,
                dto.Kapacitet
            );

            var uspesno = await _mediator.Send(command);

            if (!uspesno)
                return NotFound(_localizer["Festival_NotFound"].Value);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var uspesno = await _mediator.Send(new DeleteFestivalCommand(id));

            if (!uspesno)
                return NotFound(_localizer["Festival_NotFound"].Value);

            return Ok(_localizer["Festival_SuccessDelete"].Value);
        }
    }
}

