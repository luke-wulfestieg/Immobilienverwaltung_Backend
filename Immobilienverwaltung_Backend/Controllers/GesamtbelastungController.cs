using BE.Application.Bruttomietrenditen.Commands.CreateBruttomietrendite;
using BE.Application.Bruttomietrenditen.Commands.DeleteBruttomietrendite;
using BE.Application.Bruttomietrenditen.Commands.GetAllBruttomietrendite;
using BE.Application.Bruttomietrenditen.Commands.GetBruttomietrenditeById;
using BE.Application.Bruttomietrenditen.Commands.GetBruttomietrenditeByOverviewId;
using BE.Application.Bruttomietrenditen.Commands.UpdateBruttomietrendite;
using BE.Application.Bruttomietrenditen.DTOs;
using BE.Application.Gesamtbelastungen.Commands.CreateGesamtbelastung;
using BE.Application.Gesamtbelastungen.Commands.DeleteGesamtbelastung;
using BE.Application.Gesamtbelastungen.Commands.GetAllGesamtbelastung;
using BE.Application.Gesamtbelastungen.Commands.GetGesamtbelastungById;
using BE.Application.Gesamtbelastungen.Commands.GetGesamtbelastungByOverviewId;
using BE.Application.Gesamtbelastungen.Commands.UpdateGesamtbelastung;
using BE.Application.Gesamtbelastungen.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Immobilienverwaltung_Backend.Controllers
{
    [ApiController]
    [Route("api/overview")]
    public class GesamtbelastungController : ControllerBase
    {
        private readonly IMediator _mediator;

        public GesamtbelastungController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("gesamtbelastung")]
        public async Task<ActionResult<IEnumerable<GesamtbelastungDto>>> GetAll()
        {
            var bruttomietrenditen = await _mediator.Send(new GetAllGesamtbelastungCommand());
            return Ok(bruttomietrenditen);
        }


        [HttpGet("gesamtbelastung/{gesamtbelastungId}")]
        public async Task<ActionResult<GesamtbelastungDto>> GetById([FromRoute] int gesamtbelastungId)
        {
            var bruttomietrendite = await _mediator.Send(new GetGesamtbelastungByIdCommand(gesamtbelastungId));
            return bruttomietrendite == null ? NotFound() : Ok(bruttomietrendite);
        }

        [HttpGet("{overviewId}/gesamtbelastung")]
        public async Task<ActionResult<GesamtbelastungDto>> GetGesamtbelastungByOverviewId([FromRoute] int overviewId)
        {
            var bruttomietrendite = await _mediator.Send(new GetGesamtbelastungByOverviewIdCommand(overviewId));
            return bruttomietrendite == null ? NotFound() : Ok(bruttomietrendite);
        }

        [HttpPost("{overviewId}/gesamtbelastung")]
        public async Task<IActionResult> Create([FromRoute] int overviewId, [FromBody] CreateGesamtbelastungCommand command)
        {
            command.ImmobilienOverviewId = overviewId;
            int bruttomietrenditeId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { bruttomietrenditeId }, null);
        }

        [HttpPatch("gesamtbelastung/{gesamtbelastungId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateById([FromRoute] int gesamtbelastungId, [FromBody] UpdateGesamtbelastungCommand command)
        {
            command.Id = gesamtbelastungId;
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("gesamtbelastung/{gesamtbelastungId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteById([FromRoute] int gesamtbelastungId)
        {
            await _mediator.Send(new DeleteGesamtbelastungCommand(gesamtbelastungId));
            return NoContent();
        }
    }
}
