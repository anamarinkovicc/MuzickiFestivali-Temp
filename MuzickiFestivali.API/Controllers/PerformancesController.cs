using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MuzickiFestivali.API.DTOs;
using MuzickiFestivali.API.Features.Performances.Commands;
using MuzickiFestivali.API.Features.Performances.Queries;

namespace MuzickiFestivali.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PerformancesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public PerformancesController(IMediator mediator) => _mediator = mediator;

        [HttpPost("{idFestival}")]
        public async Task<ActionResult<int>> Create(int idFestival, NastupDto dto)
        {
            var command = new CreateNastupCommand(dto.Naziv, dto.Opis, dto.Zanr, idFestival);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("festival/{idFestival}")]
        public async Task<ActionResult<List<DisplayNastupDto>>> GetByFestival([FromRoute] int idFestival)
        {
            var result = await _mediator.Send(new GetNastupiByFestivalQuery(idFestival));
            return Ok(result);
        }

        [HttpGet("{idFestival}/{idNastup}")]
        public async Task<ActionResult<DisplayNastupDto>> GetById(int idFestival, int idNastup)
        {
            var result = await _mediator.Send(new GetNastupByIdQuery(idFestival, idNastup));

            if (result == null)
                return NotFound($"Nastup sa ID-jem {idNastup} u okviru festivala {idFestival} nije pronađen.");

            return Ok(result);
        }

        [HttpPut("{idFestival}/{idNastup}")]
        public async Task<ActionResult> Update(int idFestival, int idNastup, NastupDto dto)
        {
            var command = new UpdateNastupCommand(idFestival, idNastup, dto.Naziv, dto.Opis, dto.Zanr);
            var uspesno = await _mediator.Send(command);

            if (!uspesno)
                return NotFound("Nije moguće izmeniti nastup jer nije pronađen.");

            return NoContent();
        }

        [HttpDelete("{idFestival}/{idNastup}")]
        public async Task<ActionResult> Delete(int idFestival, int idNastup)
        {
            var uspesno = await _mediator.Send(new DeleteNastupCommand(idFestival, idNastup));

            if (!uspesno)
                return NotFound("Nastup nije pronađen, pa ne može biti obrisan.");

            return Ok("Nastup je uspešno obrisan.");
        }
    }
}
