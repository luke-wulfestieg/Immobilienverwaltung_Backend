using BE.Application.ImmobilienOverviews.Commands.CreateOverview;
using BE.Application.ImmobilienOverviews.Commands.DeleteOverview;
using BE.Application.ImmobilienOverviews.Commands.GetAllOverview;
using BE.Application.ImmobilienOverviews.Commands.GetOverviewById;
using BE.Application.ImmobilienOverviews.Commands.UpdateOverviewById;
using BE.Application.ImmobilienOverviews.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Immobilienverwaltung_Backend.Controllers
{
    [ApiController]
    [Route("api/overview")]
    public class ImmobilienOverviewController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ImmobilienOverviewController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // Get all ImmobilienOverview
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ImmobilienOverviewDto>>> GetAll()
        {
            var overviews = await _mediator.Send(new GetAllImmobilienOverviewCommand());
            return Ok(overviews);
        }

        // Get an ImmobilienOverview by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<ImmobilienOverviewDto>> GetById([FromRoute] int id)
        {
            var result = await _mediator.Send(new GetImmobilienOverviewByIdCommand(id));
            return Ok(result);
        }

        // Create a new ImmobilienOverview
        [HttpPost]
        public async Task<IActionResult> CreateImmobilienOverview(CreateImmobilienOverviewCommand command)
        {
            int id = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id }, null);
        }

        // Delete an ImmobilienOverview by ID
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteById([FromRoute] int id)
        {
            await _mediator.Send(new DeleteImmobilienOverviewByIdCommand(id));
            return NoContent();
        }

        // Update an existing ImmobilienOverview by ID
        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateById([FromRoute] int id, UpdateImmobilienOverviewCommand command)
        {
            command.Id = id;
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
