using BE.Application.Bruttomietrenditen.Commands.CreateBruttomietrendite;
using BE.Application.Bruttomietrenditen.Commands.DeleteBruttomietrendite;
using BE.Application.Bruttomietrenditen.Commands.GetAllBruttomietrendite;
using BE.Application.Bruttomietrenditen.Commands.GetBruttomietrenditeById;
using BE.Application.Bruttomietrenditen.Commands.GetBruttomietrenditeByOverviewId;
using BE.Application.Bruttomietrenditen.Commands.UpdateBruttomietrendite;
using BE.Application.Bruttomietrenditen.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Immobilienverwaltung_Backend.Controllers
{
    [ApiController]
    [Route("api/overview")]
    public class BruttomietrenditeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BruttomietrenditeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("bruttomietrendite")]
        public async Task<ActionResult<IEnumerable<BruttomietrenditeDto>>> GetAll()
        {
            var bruttomietrenditen = await _mediator.Send(new GetAllBruttomietrenditeCommand());
            return Ok(bruttomietrenditen);
        }


        [HttpGet("bruttomietrendite/{bruttomietrenditeId}")]
        public async Task<ActionResult<BruttomietrenditeDto>> GetById([FromRoute] int bruttomietrenditeId)
        {
            var bruttomietrendite = await _mediator.Send(new GetBruttomietrenditeByIdCommand(bruttomietrenditeId));
            return bruttomietrendite == null ? NotFound() : Ok(bruttomietrendite);
        }

        [HttpGet("{overviewId}/bruttomietrendite")]
        public async Task<ActionResult<BruttomietrenditeDto>> GetBruttomietrenditeByOverviewId([FromRoute] int overviewId)
        {
            var bruttomietrendite = await _mediator.Send(new GetBruttomietrenditeByOverviewIdCommand(overviewId));
            return bruttomietrendite == null ? NotFound() : Ok(bruttomietrendite);
        }

        [HttpPost("{overviewId}/bruttomietrendite")]
        public async Task<IActionResult> Create([FromRoute] int overviewId, [FromBody] CreateBruttomietrenditeCommand command)
        {
            command.ImmobilienOverviewId = overviewId;
            int bruttomietrenditeId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { bruttomietrenditeId }, null);
        }

        [HttpPatch("bruttomietrendite/{bruttomietrenditeId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateById([FromRoute] int bruttomietrenditeId, [FromBody] UpdateBruttomietrenditeByIdCommand command)
        {
            command.Id = bruttomietrenditeId;
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("bruttomietrendite/{bruttomietrenditeId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteById([FromRoute] int bruttomietrenditeId)
        {
            await _mediator.Send(new DeleteBruttomietrenditeCommand(bruttomietrenditeId));
            return NoContent();
        }
    }
}
