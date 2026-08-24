using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using MuzickiFestivali.API.DTOs;
using MuzickiFestivali.API.Features.Auth.Commands;
using MuzickiFestivali.API.Features.Users.Commands;

namespace MuzickiFestivali.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IStringLocalizer<SharedResources> _localizer;

        public UsersController(IMediator mediator, IStringLocalizer<SharedResources> localizer)
        {
            _mediator = mediator;
            _localizer = localizer;
        }


        [HttpPost("register-zaposleni")]
        public async Task<ActionResult<int>> Register(RegisterZaposleniDto dto)
        {
            var command = new RegisterZaposleniCommand(
                dto.Ime,
                dto.Prezime,
                dto.Email,
                dto.Lozinka,
                dto.Pozicija
            );
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("register-izvodjac")]
        public async Task<ActionResult<int>> RegisterIzvodjac(RegisterIzvodjacDto dto)
        {
            var command = new RegisterIzvodjacCommand(
                dto.Ime,
                dto.Prezime,
                dto.Email,
                dto.Lozinka,
                dto.UmetnickoIme,
                dto.Biografija,
                dto.Zanr
            );
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("register-korisnik")]
        public async Task<ActionResult<int>> RegisterKorisnik(RegisterKorisnikDto dto)
        {
            var command = new RegisterKorisnikCommand(
                dto.Ime,
                dto.Prezime,
                dto.Email,
                dto.Lozinka,
                dto.OmiljeniZanr
            );
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginDto dto)
        {
            var command = new LoginUserCommand(dto.Email, dto.Lozinka);
            var userId = await _mediator.Send(command);

            if (userId == null)
            {
                return Unauthorized(_localizer["User_InvalidCredentials"].Value);
            }

            HttpContext.Session.SetInt32("UserId", userId.Value);
            return Ok(new { userId = userId.Value });
        }

        [HttpPost("logout")]
        public async Task<ActionResult> Logout()
        {
            await _mediator.Send(new LogoutUserCommand());
            HttpContext.Session.Clear();
            return Ok(_localizer["User_SuccessLogout"].Value);
        }
    }

}

