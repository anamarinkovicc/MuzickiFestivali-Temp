using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using MuzickiFestivali.API.DTOs;
using MuzickiFestivali.API.Features.Appearances.Commands;
using MuzickiFestivali.API.Features.Appearances.Queries;
//using MuzickiFestivali.API.Resources;
using System.Collections.Generic;
using System.Security.Claims;
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

        [Authorize(Roles = "Zaposleni")]
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

        [Authorize(Roles = "Zaposleni")]
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

        [Authorize(Roles = "Zaposleni")]
        [HttpDelete("{idOsoba}/{idFestival}/{idNastup}/{idTermin}")]
        public async Task<ActionResult> RemoveAssignment(int idOsoba, int idFestival, int idNastup, int idTermin)
        {
            var command = new RemoveIzvodjacFromTerminCommand(idOsoba, idFestival, idNastup, idTermin);
            var uspesno = await _mediator.Send(command);

            if (!uspesno)
                return NotFound(_localizer["Appearance_NotFound"].Value);

            return Ok(_localizer["Appearance_SuccessRemoval"].Value);
        }

        [AllowAnonymous]
        [HttpGet("slot/{idFestival}/{idNastup}/{idTermin}")]
        public async Task<ActionResult<List<DisplayNastupaDto>>> GetPerformersForSlot(int idFestival, int idNastup, int idTermin)
        {
            var result = await _mediator.Send(new GetIzvodjaciByTerminQuery(idFestival, idNastup, idTermin));
            return Ok(result);
        }

        [Authorize(Roles = "Izvodjac")]
        [HttpPatch("confirm/{idFestival}/{idNastup}/{idTermin}")]
        public async Task<ActionResult> ConfirmArrival(int idFestival, int idNastup, int idTermin)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userIdClaim == null)
                return Unauthorized(_localizer["User_Unauthorized"].Value);

            int trenutniKorisnikId = int.Parse(userIdClaim);

            var command = new ConfirmArrivalCommand(
                trenutniKorisnikId,
                idFestival,
                idNastup,
                idTermin
            );

            var uspesno = await _mediator.Send(command);

            if (!uspesno)
                return NotFound(_localizer["Appearance_NotFound"].Value);

            return Ok(_localizer["Appearance_ArrivalConfirmed"].Value);
        }

        [Authorize(Roles = "Izvodjac")]
        [HttpGet("my-schedule")]
        public async Task<ActionResult<List<DisplayMyAppearanceDto>>> GetMySchedule()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userIdClaim == null)
                return Unauthorized(_localizer["User_Unauthorized"].Value);

            //int trenutniKorisnikId = int.Parse(userIdClaim);

            if (!int.TryParse(userIdClaim, out int trenutniKorisnikId))
                return Unauthorized(_localizer["User_Unauthorized"].Value);

            var query = new GetMyAppearancesQuery(trenutniKorisnikId);
            var result = await _mediator.Send(query);

            return Ok(result);
        }
    }
}

