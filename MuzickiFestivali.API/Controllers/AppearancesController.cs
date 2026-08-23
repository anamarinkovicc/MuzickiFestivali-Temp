using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MuzickiFestivali.API.DTOs;
using MuzickiFestivali.API.Features.Appearances.Commands;
using MuzickiFestivali.API.Features.Appearances.Queries;

namespace MuzickiFestivali.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppearancesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AppearancesController(IMediator mediator) => _mediator = mediator;

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
                return NotFound("Izvođač ili termin nisu pronađeni.");

            return Ok("Izvođač je uspešno raspoređen u termin. Status dolaska je postavljen na 'Nepotvrđen'.");
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
                return NotFound("Zapis o nastupanju nije pronađen.");

            return NoContent();
        }

        [HttpDelete("{idOsoba}/{idFestival}/{idNastup}/{idTermin}")]
        public async Task<ActionResult> RemoveAssignment(int idOsoba, int idFestival, int idNastup, int idTermin)
        {
            var command = new RemoveIzvodjacFromTerminCommand(idOsoba, idFestival, idNastup, idTermin);
            var uspesno = await _mediator.Send(command);

            if (!uspesno)
                return NotFound("Zapis o nastupanju nije pronađen.");

            return Ok("Izvođač je uspešno uklonjen iz termina.");
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
                return Unauthorized("Morate biti prijavljeni da biste potvrdili dolazak.");

            var command = new ConfirmArrivalCommand(
                trenutniKorisnikId.Value,
                idFestival,
                idNastup,
                idTermin
            );

            var uspesno = await _mediator.Send(command);

            if (!uspesno)
                return NotFound("Nije pronađen angažman za vaš nalog na ovom terminu.");

            return Ok("Uspešno ste potvrdili svoj dolazak!");
        }
        [HttpGet("my-schedule")]
        public async Task<ActionResult<List<DisplayMyAppearanceDto>>> GetMySchedule()
        {
            int? trenutniKorisnikId = HttpContext.Session.GetInt32("UserId");

            if (trenutniKorisnikId == null)
                return Unauthorized("Morate biti prijavljeni da biste videli svoj raspored nastupa.");

            var query = new GetMyAppearancesQuery(trenutniKorisnikId.Value);
            var result = await _mediator.Send(query);

            return Ok(result);
        }
    }
}

