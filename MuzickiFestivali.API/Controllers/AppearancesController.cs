using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using MuzickiFestivali.API.DTOs;
using MuzickiFestivali.API.Features.Appearances.Commands;
using MuzickiFestivali.API.Features.Appearances.Queries;
//using MuzickiFestivali.API.Resources;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MuzickiFestivali.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppearancesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IStringLocalizer<SharedResources> _localizer;

        public AppearancesController(IMediator mediator, IStringLocalizer<SharedResources> localizer)
        {
            _mediator = mediator;
            _localizer = localizer;
        }


        [HttpPost("{idOsoba}/{idFestival}/{idNastup}/{idTermin}")]
        public async Task<ActionResult> AssignPerformer(int idOsoba, int idFestival, int idNastup, int idTermin, NastupaDto dto)
        {
            var command = new AssignIzvodjacToTerminCommand(
                idOsoba,
                idFestival,
                idNastup,
                idTermin,
                dto.Uloga,
                dto.Napomena
            );

            var uspesno = await _mediator.Send(command);
            if (!uspesno)
                return NotFound(_localizer["Appearance_PerformerOrSlotNotFound"].Value);

            return Ok(_localizer["Appearance_SuccessAssignment"].Value);
        }

        [HttpPut("{idOsoba}/{idFestival}/{idNastup}/{idTermin}")]
        public async Task<ActionResult> UpdateAssignment(int idOsoba, int idFestival, int idNastup, int idTermin, UpdateNastupaDto dto)
        {
            var command = new UpdateNastupaCommand(
                idOsoba,
                idFestival,
                idNastup,
                idTermin,
                dto.Uloga,
                dto.PotvrdjenDolazak,
                dto.Napomena
            );

            var uspesno = await _mediator.Send(command);
            if (!uspesno)
                return NotFound(_localizer["Appearance_NotFound"].Value);

            return NoContent();
        }

        [HttpDelete("{idOsoba}/{idFestival}/{idNastup}/{idTermin}")]
        public async Task<ActionResult> RemoveAssignment(int idOsoba, int idFestival, int idNastup, int idTermin)
        {
            var command = new RemoveIzvodjacFromTerminCommand(idOsoba, idFestival, idNastup, idTermin);
            var uspesno = await _mediator.Send(command);

            if (!uspesno)
                return NotFound(_localizer["Appearance_NotFound"].Value);

            return Ok(_localizer["Appearance_SuccessRemoval"].Value);
        }

        [HttpGet("slot/{idFestival}/{idNastup}/{idTermin}")]
        public async Task<ActionResult<List<DisplayNastupaDto>>> GetPerformersForSlot(int idFestival, int idNastup, int idTermin)
        {
            var result = await _mediator.Send(new GetIzvodjaciByTerminQuery(idFestival, idNastup, idTermin));
            return Ok(result);
        }

        [HttpPatch("confirm/{idFestival}/{idNastup}/{idTermin}")]
        public async Task<ActionResult> ConfirmArrival(int idFestival, int idNastup, int idTermin)
        {
            int? trenutniKorisnikId = HttpContext.Session.GetInt32("UserId");

            if (trenutniKorisnikId == null)
                return Unauthorized(_localizer["User_Unauthorized"].Value);

            var command = new ConfirmArrivalCommand(
                trenutniKorisnikId.Value,
                idFestival,
                idNastup,
                idTermin
            );

            var uspesno = await _mediator.Send(command);

            if (!uspesno)
                return NotFound(_localizer["Appearance_NotFound"].Value);

            return Ok(_localizer["Appearance_ArrivalConfirmed"].Value);
        }
        [HttpGet("my-schedule")]
        public async Task<ActionResult<List<DisplayMyAppearanceDto>>> GetMySchedule()
        {
            int? trenutniKorisnikId = HttpContext.Session.GetInt32("UserId");

            if (trenutniKorisnikId == null)
                return Unauthorized(_localizer["User_Unauthorized"].Value);

            var query = new GetMyAppearancesQuery(trenutniKorisnikId.Value);
            var result = await _mediator.Send(query);

            return Ok(result);
        }
    }
}

