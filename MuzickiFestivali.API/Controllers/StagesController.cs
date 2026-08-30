using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using MuzickiFestivali.API.DTOs;
using MuzickiFestivali.API.Features.Stages.Queries;

namespace MuzickiFestivali.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StagesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IStringLocalizer<SharedResources> _localizer;

        public StagesController(IMediator mediator, IStringLocalizer<SharedResources> localizer)
        {
            _mediator = mediator;
            _localizer = localizer;
        }

        [HttpGet]
        public async Task<ActionResult<List<DisplayBinaDto>>> GetAll()
        {
            var result = await _mediator.Send(new GetAllStagesQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DisplayBinaDto>> GetById(int id)
        {
            var result = await _mediator.Send(new GetStageByIdQuery(id));

            if (result == null)
            {
                var message = _localizer["Stage_NotFound"]?.Value ?? "Bina nije pronađena.";
                return NotFound(message);
            }

            return Ok(result);
        }
    }
}

