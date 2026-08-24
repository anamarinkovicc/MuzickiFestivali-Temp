using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
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
        private readonly IStringLocalizer<SharedResources> _localizer;

        public LikesController(IMediator mediator, IStringLocalizer<SharedResources> localizer)
        {
            _mediator = mediator;
            _localizer = localizer;
        }

        [HttpPost("performance/{idFestival}/{idNastup}")]
        public async Task<ActionResult> Like(int idFestival, int idNastup)
        {
            int? trenutniKorisnikId = HttpContext.Session.GetInt32("UserId");

            if (trenutniKorisnikId == null)
                return Unauthorized(_localizer["User_Unauthorized"].Value);

            var command = new LikeNastupCommand(idFestival, idNastup, trenutniKorisnikId.Value);
            var uspesno = await _mediator.Send(command);

            if (!uspesno)
                return NotFound(_localizer["Performance_NotFound"].Value);

            return Ok(_localizer["Like_SuccessAdd"].Value);
        }

        [HttpDelete("performance/{idFestival}/{idNastup}")]
        public async Task<ActionResult> Unlike(int idFestival, int idNastup)
        {
            int? trenutniKorisnikId = HttpContext.Session.GetInt32("UserId");

            if (trenutniKorisnikId == null)
                return Unauthorized(_localizer["User_Unauthorized"].Value);

            var command = new UnlikeNastupCommand(idFestival, idNastup, trenutniKorisnikId.Value);
            var uspesno = await _mediator.Send(command);

            if (!uspesno)
                return NotFound(_localizer["Like_NotFound"].Value);

            return Ok(_localizer["Like_SuccessRemove"].Value);
        }

        [HttpGet("my-liked-performances")]
        public async Task<ActionResult<List<DisplayLikedPerformanceDto>>> GetMyLikedPerformances()
        {
            int? trenutniKorisnikId = HttpContext.Session.GetInt32("UserId");

            if (trenutniKorisnikId == null)
                return Unauthorized(_localizer["User_Unauthorized"].Value);

            var query = new GetMyLikedNastupiQuery(trenutniKorisnikId.Value);
            var result = await _mediator.Send(query);

            return Ok(result);
        }
    }
}

