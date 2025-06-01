using BE.Application.Bruttomietrenditen.Commands.CreateBruttomietrendite;
using BE.Application.Bruttomietrenditen.Commands.DeleteBruttomietrendite;
using BE.Application.Bruttomietrenditen.Commands.GetAllBruttomietrendite;
using BE.Application.Bruttomietrenditen.Commands.GetBruttomietrenditeById;
using BE.Application.Bruttomietrenditen.Commands.GetBruttomietrenditeByOverviewId;
using BE.Application.Bruttomietrenditen.Commands.UpdateBruttomietrendite;
using BE.Application.Bruttomietrenditen.DTOs;
using BE.Application.Ruecklagen.Commands.CreateRuecklagen;
using BE.Application.Ruecklagen.Commands.DeleteRuecklagen;
using BE.Application.Ruecklagen.Commands.GeRuecklagenById;
using BE.Application.Ruecklagen.Commands.GetAllRuecklagen;
using BE.Application.Ruecklagen.Commands.GetRuecklagenyOverviewId;
using BE.Application.Ruecklagen.Commands.UpdateRuecklagen;
using BE.Application.Ruecklagen.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Immobilienverwaltung_Backend.Controllers
{
    [ApiController]
    [Route("api/overview")]
    public class RuecklagenController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RuecklagenController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("ruecklagen")]
        public async Task<ActionResult<IEnumerable<RuecklagenDto>>> GetAll()
        {
            var ruecklagen = await _mediator.Send(new GetAllRuecklagenCommand());
            return Ok(ruecklagen);
        }


        [HttpGet("ruecklagen/{ruecklagenId}")]
        public async Task<ActionResult<RuecklagenDto>> GetById([FromRoute] int ruecklagenId)
        {
            var ruecklage = await _mediator.Send(new GetRuecklagenByIdCommand(ruecklagenId));
            return ruecklage == null ? NotFound() : Ok(ruecklage);
        }

        [HttpGet("{overviewId}/ruecklagen")]
        public async Task<ActionResult<RuecklagenDto>> GetRuecklagenByOverviewId([FromRoute] int overviewId)
        {
            var ruecklage = await _mediator.Send(new GetRuecklagenByOverviewIdCommand(overviewId));
            return ruecklage == null ? NotFound() : Ok(ruecklage);
        }

        [HttpPost("{overviewId}/ruecklagen")]
        public async Task<IActionResult> Create([FromRoute] int overviewId, [FromBody] CreateRuecklagenCommand command)
        {
            command.ImmobilienOverviewId = overviewId;
            int ruecklagenId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { ruecklagenId }, null);
        }

        [HttpPatch("ruecklagen/{ruecklagenId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateById([FromRoute] int ruecklagenId, [FromBody] UpdateRuecklagenCommand command)
        {
            command.Id = ruecklagenId;
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("ruecklagen/{ruecklagenId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteById([FromRoute] int ruecklagenId)
        {
            await _mediator.Send(new DeleteRuecklagenCommand(ruecklagenId));
            return NoContent();
        }
    }
}
