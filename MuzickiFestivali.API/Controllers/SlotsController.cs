using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using MuzickiFestivali.API.DTOs;
using MuzickiFestivali.API.Features.Slots.Commands;
using MuzickiFestivali.API.Features.Slots.Queries;

namespace MuzickiFestivali.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SlotsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IStringLocalizer<SharedResources> _localizer;

        public SlotsController(IMediator mediator, IStringLocalizer<SharedResources> localizer)
        {
            _mediator = mediator;
            _localizer = localizer;
        }

        [Authorize(Roles = "Zaposleni")]
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

        [Authorize(Roles = "Zaposleni,Izvodjac")]
        [HttpGet("{idFestival}/{idNastup}")]
        public async Task<ActionResult<List<DisplayTerminDto>>> GetByNastup(int idFestival, int idNastup)
        {
            var result = await _mediator.Send(new GetTerminiByNastupQuery(idFestival, idNastup));
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet("{idFestival}/{idNastup}/{idTermin}")]
        public async Task<ActionResult<DisplayTerminDto>> GetById(int idFestival, int idNastup, int idTermin)
        {
            var result = await _mediator.Send(new GetTerminByIdQuery(idFestival, idNastup, idTermin));

            if (result == null)
                return NotFound(_localizer["Slot_NotFound"].Value);

            return Ok(result);
        }

        [Authorize(Roles = "Zaposleni")]
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
                return NotFound(_localizer["Slot_NotFound"].Value);

            return NoContent();
        }

        [Authorize(Roles = "Zaposleni")]
        [HttpDelete("{idFestival}/{idNastup}/{idTermin}")]
        public async Task<ActionResult> Delete(int idFestival, int idNastup, int idTermin)
        {
            var uspesno = await _mediator.Send(new DeleteTerminCommand(idFestival, idNastup, idTermin));

            if (!uspesno)
                return NotFound(_localizer["Slot_NotFound"].Value);

            return Ok(_localizer["Slot_SuccessDelete"].Value);
        }

        [AllowAnonymous]
        [HttpGet("public/{idFestival}/{idNastup}")]
        public async Task<ActionResult<List<DisplayTerminDto>>> GetGlavniByNastup(int idFestival, int idNastup)
        {
            var result = await _mediator.Send(new GetGlavniTerminiByNastupQuery(idFestival, idNastup));
            return Ok(result);
        }
    }
}

