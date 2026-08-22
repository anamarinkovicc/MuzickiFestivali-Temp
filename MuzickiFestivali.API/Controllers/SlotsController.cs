using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MuzickiFestivali.API.DTOs;
using MuzickiFestivali.API.Features.Slots.Commands;
using MuzickiFestivali.API.Features.Slots.Queries;

namespace MuzickiFestivali.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SlotsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public SlotsController(IMediator mediator) => _mediator = mediator;

        [HttpPost("{idFestival}/{idNastup}")]
        public async Task<ActionResult<int>> Create(int idFestival, int idNastup, TerminDto dto)
        {
            var command = new CreateTerminCommand(
                idFestival,
                idNastup,
                dto.VremePocetka,
                dto.VremeZavrsetka,
                dto.Tip,
                dto.IdBina,
                dto.Napomena
            );
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("{idFestival}/{idNastup}")]
        public async Task<ActionResult<List<DisplayTerminDto>>> GetByNastup(int idFestival, int idNastup)
        {
            var result = await _mediator.Send(new GetTerminiByNastupQuery(idFestival, idNastup));
            return Ok(result);
        }

        [HttpGet("{idFestival}/{idNastup}/{idTermin}")]
        public async Task<ActionResult<DisplayTerminDto>> GetById(int idFestival, int idNastup, int idTermin)
        {
            var result = await _mediator.Send(new GetTerminByIdQuery(idFestival, idNastup, idTermin));

            if (result == null)
                return NotFound("Termin nije pronađen.");

            return Ok(result);
        }

        [HttpPut("{idFestival}/{idNastup}/{idTermin}")]
        public async Task<ActionResult> Update(int idFestival, int idNastup, int idTermin, TerminDto dto)
        {
            var command = new UpdateTerminCommand(
                idFestival,
                idNastup,
                idTermin,
                dto.VremePocetka,
                dto.VremeZavrsetka,
                dto.Tip,
                dto.IdBina,
                dto.Napomena
            );

            var uspesno = await _mediator.Send(command);

            if (!uspesno)
                return NotFound("Termin nije pronađen.");

            return NoContent();
        }

        [HttpDelete("{idFestival}/{idNastup}/{idTermin}")]
        public async Task<ActionResult> Delete(int idFestival, int idNastup, int idTermin)
        {
            var uspesno = await _mediator.Send(new DeleteTerminCommand(idFestival, idNastup, idTermin));

            if (!uspesno)
                return NotFound("Termin nije pronađen.");

            return Ok("Termin je uspešno obrisan.");
        }
    }
}

