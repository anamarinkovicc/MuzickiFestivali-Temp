using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using MuzickiFestivali.API.DTOs;
using MuzickiFestivali.API.Features.Likes.Commands;
using MuzickiFestivali.API.Features.Likes.Queries;
using System.Security.Claims;

namespace MuzickiFestivali.API.Controllers
{
    [Authorize(Roles = "Korisnik")]
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
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userIdClaim == null)
                return Unauthorized(_localizer["User_Unauthorized"].Value);

            int trenutniKorisnikId = int.Parse(userIdClaim);


            var command = new LikeNastupCommand(idFestival, idNastup, trenutniKorisnikId);
            var uspesno = await _mediator.Send(command);

            if (!uspesno)
                return NotFound(_localizer["Performance_NotFound"].Value);

            return Ok(_localizer["Like_SuccessAdd"].Value);
        }

        [HttpDelete("performance/{idFestival}/{idNastup}")]
        public async Task<ActionResult> Unlike(int idFestival, int idNastup)
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (userIdClaim == null)
                return Unauthorized(_localizer["User_Unauthorized"].Value);

            int trenutniKorisnikId = int.Parse(userIdClaim);


            var command = new UnlikeNastupCommand(idFestival, idNastup, trenutniKorisnikId);
            var uspesno = await _mediator.Send(command);

            if (!uspesno)
                return NotFound(_localizer["Like_NotFound"].Value);

            return Ok(_localizer["Like_SuccessRemove"].Value);
        }

        [HttpGet("my-liked-performances")]
        public async Task<ActionResult<List<DisplayLikedPerformanceDto>>> GetMyLikedPerformances()
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (userIdClaim == null)
                return Unauthorized(_localizer["User_Unauthorized"].Value);

            int trenutniKorisnikId = int.Parse(userIdClaim);


            var query = new GetMyLikedNastupiQuery(trenutniKorisnikId);
            var result = await _mediator.Send(query);

            return Ok(result);
        }
    }
}

