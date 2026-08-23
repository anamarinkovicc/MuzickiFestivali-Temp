using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MuzickiFestivali.API.DTOs;
using MuzickiFestivali.API.Features.Likes.Commands;
using MuzickiFestivali.API.Features.Likes.Queries;

namespace MuzickiFestivali.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LikesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LikesController(IMediator mediator) => _mediator = mediator;

        [HttpPost("performance/{idFestival}/{idNastup}")]
        public async Task<ActionResult> Like(int idFestival, int idNastup)
        {
            int? trenutniKorisnikId = HttpContext.Session.GetInt32("UserId");

            if (trenutniKorisnikId == null)
                return Unauthorized("Morate biti prijavljeni da biste lajkovali nastup.");

            var command = new LikeNastupCommand(idFestival, idNastup, trenutniKorisnikId.Value);
            var uspesno = await _mediator.Send(command);

            if (!uspesno)
                return NotFound("Nastup nije pronađen.");

            return Ok("Nastup je uspešno lajkovan.");
        }

        [HttpDelete("performance/{idFestival}/{idNastup}")]
        public async Task<ActionResult> Unlike(int idFestival, int idNastup)
        {
            int? trenutniKorisnikId = HttpContext.Session.GetInt32("UserId");

            if (trenutniKorisnikId == null)
                return Unauthorized("Morate biti prijavljeni da biste povukli lajk.");

            var command = new UnlikeNastupCommand(idFestival, idNastup, trenutniKorisnikId.Value);
            var uspesno = await _mediator.Send(command);

            if (!uspesno)
                return NotFound("Lajk nije pronađen.");

            return Ok("Lajk je uspešno povučen.");
        }

        [HttpGet("my-liked-performances")]
        public async Task<ActionResult<List<DisplayLikedPerformanceDto>>> GetMyLikedPerformances()
        {
            int? trenutniKorisnikId = HttpContext.Session.GetInt32("UserId");

            if (trenutniKorisnikId == null)
                return Unauthorized("Morate biti prijavljeni da biste videli svoje lajkovane nastupe.");

            var query = new GetMyLikedNastupiQuery(trenutniKorisnikId.Value);
            var result = await _mediator.Send(query);

            return Ok(result);
        }
    }
}

