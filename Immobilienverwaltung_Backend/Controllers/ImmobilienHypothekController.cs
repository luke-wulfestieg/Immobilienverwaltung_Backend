using BE.Application.ImmobilienHausgelder.DTOs;
using BE.Application.ImmobilienHypotheken.Commands.CreateHypothek;
using BE.Application.ImmobilienHypotheken.Commands.DeleteHypothek;
using BE.Application.ImmobilienHypotheken.Commands.GetAllHypothek;
using BE.Application.ImmobilienHypotheken.Commands.GetHypothekById;
using BE.Application.ImmobilienHypotheken.Commands.GetHypothekByOverviewId;
using BE.Application.ImmobilienHypotheken.Commands.UpdateHypothek;
using BE.Application.ImmobilienHypotheken.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Immobilienverwaltung_Backend.Controllers
{
    [ApiController]
    [Route("api/overview")]
    public class ImmobilienHypothekController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ImmobilienHypothekController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("hypothek")]
        public async Task<ActionResult<IEnumerable<ImmobilienHypothekDto>>> GetAll()
        {
            var hypothek = await _mediator.Send(new GetAllImmobilienHypothekCommand());
            return Ok(hypothek);
        }
        [HttpGet("{overviewId}/hypothek")]
        public async Task<ActionResult<ImmobilienHypothekDto>> GetHypothekByOverviewId([FromRoute] int overviewId)
        {
            var hypothek = await _mediator.Send(new GetImmobilienHypothekByOverviewIdCommand(overviewId));
            return hypothek == null ? NotFound() : Ok(hypothek);
        }

        [HttpGet("hypothek/{hypothekId}")]
        public async Task<ActionResult<ImmobilienHausgeldDto>> GetById([FromRoute] int hypothekId)
        {
            var hypothek = await _mediator.Send(new GetImmobilienHypothekByIdCommand(hypothekId));
            return hypothek == null ? NotFound() : Ok(hypothek);
        }

        [HttpPost("{overviewId}/hypohek")]
        public async Task<IActionResult> Create([FromRoute] int overviewId, [FromBody] CreateImmobilienHypothekCommand command)
        {
            command.ImmobilienOverviewId = overviewId;
            int hypothekId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { hypothekId }, null);
        }

        [HttpPatch("hypothek/{hypothekId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateById([FromRoute] int hypothekId, [FromBody] UpdateImmobilienHypothekByIdCommand command)
        {
            command.Id = hypothekId;
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("hypothek/{hyothekId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteById([FromRoute] int hyothekId)
        {
            await _mediator.Send(new DeleteImmobilienHypothekByIdCommand(hyothekId));
            return NoContent();
        }

    }
}
