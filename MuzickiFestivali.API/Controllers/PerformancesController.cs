using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using MuzickiFestivali.API.DTOs;
using MuzickiFestivali.API.Features.Performances.Commands;
using MuzickiFestivali.API.Features.Performances.Queries;

namespace MuzickiFestivali.API.Controllers
{
    [Authorize(Roles = "Zaposleni")]
    [Route("api/[controller]")]
    [ApiController]
    public class PerformancesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IStringLocalizer<SharedResources> _localizer;

        public PerformancesController(IMediator mediator, IStringLocalizer<SharedResources> localizer)
        {
            _mediator = mediator;
            _localizer = localizer;
        }

        [HttpPost("{idFestival}")]
        public async Task<ActionResult<int>> Create(int idFestival, NastupDto dto)
        {
            var command = new CreateNastupCommand(dto.Naziv, dto.Opis, dto.Zanr, idFestival);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet("festival/{idFestival}")]
        public async Task<ActionResult<List<DisplayNastupDto>>> GetByFestival([FromRoute] int idFestival)
        {
            var result = await _mediator.Send(new GetNastupiByFestivalQuery(idFestival));
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet("{idFestival}/{idNastup}")]
        public async Task<ActionResult<DisplayNastupDto>> GetById(int idFestival, int idNastup)
        {
            var result = await _mediator.Send(new GetNastupByIdQuery(idFestival, idNastup));

            if (result == null)
                return NotFound(_localizer["Performance_NotFound"].Value);

            return Ok(result);
        }

        [HttpPut("{idFestival}/{idNastup}")]
        public async Task<ActionResult> Update(int idFestival, int idNastup, NastupDto dto)
        {
            var command = new UpdateNastupCommand(idFestival, idNastup, dto.Naziv, dto.Opis, dto.Zanr);
            var uspesno = await _mediator.Send(command);

            if (!uspesno)
                return NotFound(_localizer["Performance_NotFound"].Value);

            return NoContent();
        }

        [HttpDelete("{idFestival}/{idNastup}")]
        public async Task<ActionResult> Delete(int idFestival, int idNastup)
        {
            var uspesno = await _mediator.Send(new DeleteNastupCommand(idFestival, idNastup));

            if (!uspesno)
                return NotFound(_localizer["Performance_NotFound"].Value);

            return Ok(_localizer["Performance_SuccessDelete"].Value);
        }
    }
}
