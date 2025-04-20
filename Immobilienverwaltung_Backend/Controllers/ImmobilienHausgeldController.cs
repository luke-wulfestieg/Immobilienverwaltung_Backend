using BE.Application.ImmobilienHausgelder.Commands.CreateHausgeld;
using BE.Application.ImmobilienHausgelder.Commands.DeleteHausgeld;
using BE.Application.ImmobilienHausgelder.Commands.GetAllHausgeld;
using BE.Application.ImmobilienHausgelder.Commands.GetHausgeldById;
using BE.Application.ImmobilienHausgelder.Commands.UpdateHausgeld;
using BE.Application.ImmobilienHausgelder.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Immobilienverwaltung_Backend.Controllers
{
    [ApiController]
    [Route("api/overview")]
    public class ImmobilienHausgeldController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ImmobilienHausgeldController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("{overviewId}/hausgeld")]
        public async Task<IActionResult> Create([FromRoute] int overviewId, [FromBody] CreateImmobilienHausgeldCommand command)
        {
            command.ImmobilienOverviewId = overviewId;
            int hausgeldId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { overviewId, hausgeldId }, null);
        }

        [HttpGet("hausgeld")]
        public async Task<ActionResult<IEnumerable<ImmobilienHausgeldDto>>> GetAll()
        {
            var hausgeld = await _mediator.Send(new GetAllImmobilienHausgeldCommand());
            return Ok(hausgeld);
        }

        [HttpGet("hausgeld/{hausgeldId}")]
        public async Task<ActionResult<ImmobilienHausgeldDto>> GetById([FromRoute] int hausgeldId)
        {
            var hausgeld = await _mediator.Send(new GetImmobilienHausgeldByIdCommand(null ,hausgeldId));
            return hausgeld == null ? NotFound() : Ok(hausgeld);
        }

        [HttpPatch("hausgeld/{hausgeldId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateById([FromRoute] int hausgeldId, [FromBody] UpdateImmobilienHausgeldByIdCommand command)
        {
            command.Id = hausgeldId;
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("hausgeld/{hausgeldId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteById([FromRoute] int hausgeldId)
        {
            await _mediator.Send(new DeleteImmobilienHausgeldCommand(hausgeldId));
            return NoContent();
        }
    }
}
