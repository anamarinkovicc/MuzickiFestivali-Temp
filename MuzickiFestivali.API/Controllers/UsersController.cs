using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.IdentityModel.Tokens;
using MuzickiFestivali.API.DTOs;
using MuzickiFestivali.API.Features.Auth.Commands;
using MuzickiFestivali.API.Features.Users.Commands;
using MuzickiFestivali.API.Features.Users.Queries;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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

        [Authorize(Roles = "Zaposleni")]
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
        public async Task<ActionResult> Login([FromBody] LoginDto dto)
        {
            var command = new LoginUserCommand(dto.Email, dto.Lozinka);
            var loggedUser = await _mediator.Send(command);

            if (loggedUser == null)
            {
                return Unauthorized(_localizer["User_InvalidCredentials"].Value);
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, loggedUser.IdOsoba.ToString()),
                new Claim(ClaimTypes.Email, loggedUser.Email),
                new Claim(ClaimTypes.Role, loggedUser.Uloga), 
                new Claim("Ime", loggedUser.Ime),
                new Claim("Prezime", loggedUser.Prezime)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("TvojJakoDugacakITajniKljucKojiImaBar32Karaktera"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "MuzickiFestivaliBackend",
                audience: "MuzickiFestivaliReact",
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(new
            {
                Token = tokenString,
                User = new
                {
                    idOsoba = loggedUser.IdOsoba,
                    email = loggedUser.Email,
                    uloga = loggedUser.Uloga,
                    ime = loggedUser.Ime,
                    prezime = loggedUser.Prezime
                }
            });
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<DisplayUserDto>> GetById(int id)
        {
            var result = await _mediator.Send(new GetOsobaByIdQuery(id));
            if (result == null) return NotFound(_localizer["User_NotFound"]?.Value ?? "Korisnik nije pronađen.");
            return Ok(result);
        }

        [HttpGet("izvodjaci")]
        public async Task<ActionResult<List<DisplayIzvodjacDto>>> GetAllIzvodjaci()
        {
            var query = new GetAllIzvodjaciQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }

}

